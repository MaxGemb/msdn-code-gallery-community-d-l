﻿Imports ComponentPro.IO

Namespace ClientSample
	''' <summary>
	''' Defines client plugin.
	''' </summary>
	Friend Interface IClientPlugin
		''' <summary>
		''' Create a new <see cref="IRemoteFileSystem"/> instance.
		''' </summary>
		Function Create() As IRemoteFileSystem

		''' <summary>
		''' Connects to the server.
		''' </summary>
		Sub Connect()

		''' <summary>
		''' Authenticate the user.
		''' </summary>
		Sub Authenticate()

		''' <summary>
		''' This method is invoked when the user has been successfully authenticated.
		''' </summary>
		Sub OnAuthenticated()

		''' <summary>
		''' Checks to see if the specified exception indicates that the connection has been closed and the client should reconnect.
		''' </summary>
		''' <param name="ex">The recent exception.</param>
		''' <returns><c>true</c> to reconnect; otherwise <c>false</c>.</returns>
		Function ShouldReconnect(ByVal ex As Exception) As Boolean

		''' <summary>
		''' Gets platform-specific permission string.
		''' </summary>
		''' <param name="f">The remote file info.</param>
		''' <returns>The permission string</returns>
		Function GetPermissionsString(ByVal f As AbstractFileInfo) As String

		''' <summary>
		''' Applies the general settings.
		''' </summary>
		''' <param name="settings">The general settings.</param>
		Sub ApplySettings(ByVal settings As SettingInfoBase)

		''' <summary>
		''' Applies the login settings.
		''' </summary>
		''' <param name="settings">The login settings.</param>
		Sub ApplyLoginSettings(ByVal settings As SettingInfoBase)
	End Interface

	''' <summary>
	''' Defines the server protocols
	''' </summary>
	Friend Enum ServerProtocol
		''' <summary>
		''' FTP.
		''' </summary>
		Ftp

		''' <summary>
		''' SFTP.
		''' </summary>
		Sftp
	End Enum

	''' <summary>
	''' Represents the FTP or SFTP Client Controller
	''' </summary>
	Partial Friend Class ClientController
		Protected _view As IClientView
		Protected _client As IRemoteFileSystem
		Protected _clientPlugin As IClientPlugin
		Protected _loginInfo As SettingInfoBase
		Protected _settings As SettingInfoBase

		''' <summary>
		''' Gets the current state.
		''' </summary>
		Public ReadOnly Property State() As ConnectionState
			Get
				Return _state
			End Get
		End Property

		''' <summary>
		''' Gets the login settings.
		''' </summary>
		Public ReadOnly Property LoginSettings() As SettingInfoBase
			Get
				Return _loginInfo
			End Get
		End Property

		''' <summary>
		''' Gets the general settings.
		''' </summary>
		Public ReadOnly Property Settings() As SettingInfoBase
			Get
				Return _settings
			End Get
		End Property

		''' <summary>
		''' Gets the protocol of the server.
		''' </summary>
		Public ReadOnly Property ServerType() As ServerProtocol
			Get
				Return _serverType
			End Get
		End Property

		''' <summary>
		''' Indicates whether the user has cancelled the current operation.
		''' </summary>
		Public ReadOnly Property IsAborting() As Boolean
			Get
				Return _aborting
			End Get
		End Property

		''' <summary>
		''' Gets the client object.
		''' </summary>
		Public ReadOnly Property Client() As IRemoteFileSystem
			Get
				Return _client
			End Get
		End Property

		''' <summary>
		''' Initializes the <see cref="ClientController"/>
		''' </summary>
		''' <param name="view">The <see cref="IClientView"/>.</param>
		''' <param name="loginSettings">The login settings.</param>
		''' <param name="settings">The general settings.</param>
		Public Sub New(ByVal view As IClientView, ByVal loginSettings As SettingInfoBase, ByVal settings As SettingInfoBase)
			_view = view
			_loginInfo = loginSettings
			_settings = settings

			_currentLocalDirectory = loginSettings.Get(Of String)(LoginInfo.LocalDir)
		End Sub

		Public Sub ApplySettings(ByVal settings As SettingInfoBase)
			If _clientPlugin IsNot Nothing Then
				_client.RestoreFileProperties = settings.Get(Of Boolean)(SettingInfo.RestoreFileProperties)
				_clientPlugin.ApplySettings(settings)
			End If
		End Sub

		Public Sub ApplyLoginSettings(ByVal loginSettings As SettingInfoBase)
			If _clientPlugin IsNot Nothing Then
				_clientPlugin.ApplyLoginSettings(loginSettings)
			End If
		End Sub

		Private Function GetSpeedInfo(ByVal operation As String, ByVal e As FileSystemProgressEventArgs) As String
			If e.BytesPerSecond <> 0 Then
				Return String.Format("{3} file {0}... {1}/sec {2} remaining", e.SourceFileInfo.Name, Util.FormatSize(e.BytesPerSecond), e.RemainingTime, operation)
			End If

			Return String.Format("{1} file {0}...", e.SourceFileInfo.Name, operation)
		End Function

		Friend Sub EnableProgress(ByVal enable As Boolean)
			_aborting = False
			_view.EnableProgress(enable)
		End Sub

		''' <summary>
		''' Displays progress information of the current operation.
		''' </summary>
		Private Sub client_Progress(ByVal sender As Object, ByVal e As FileSystemProgressEventArgs)
			Select Case e.State
				Case TransferState.DeletingDirectory
					' It's about to delete a directory. To skip deleting this directory, simply set e.Skip = true.
					_view.UpdateStatus(String.Format("Deleting directory {0}...", e.SourceFileSystem.GetFileName(e.SourcePath)))
					If _settings.Get(Of Boolean)(SettingInfo.ShowProgressWhileDeleting) Then
						'progressBarTotal.Value = (int)e.TotalPercentage;                    
						_view.UpdateProgress(e, False, True)
					End If
					Return

				Case TransferState.DeletingFile
					' It's about to delete a file. To skip deleting this file, simply set e.Skip = true.
					_view.UpdateStatus(String.Format("Deleting file {0}...", e.SourceFileSystem.GetFileName(e.SourcePath)))
					If _settings.Get(Of Boolean)(SettingInfo.ShowProgressWhileDeleting) Then
						'progressBarTotal.Value = (int)e.TotalPercentage;                    
						_view.UpdateProgress(e, False, True)
					End If
					Return

				Case TransferState.SettingFileAttribute
					' It's about to setting attributes of a file. To skip this file, simply set e.Skip = true.
					_view.UpdateStatus(String.Format("Setting attributes file {0}...", e.SourceFileSystem.GetFileName(e.SourcePath)))
					If _settings.Get(Of Boolean)(SettingInfo.ShowProgressWhileDeleting) Then
						'progressBarTotal.Value = (int)e.TotalPercentage;
						_view.UpdateProgress(e, False, True)
					End If
					Return

				Case TransferState.SettingFilePermission
					' It's about to setting attributes of a file. To skip this file, simply set e.Skip = true.
					_view.UpdateStatus(String.Format("Setting permissions file {0}...", e.SourceFileSystem.GetFileName(e.SourcePath)))
					If _settings.Get(Of Boolean)(SettingInfo.ShowProgressWhileDeleting) Then
						'progressBarTotal.Value = (int)e.TotalPercentage;
						_view.UpdateProgress(e, False, True)
					End If
					Return

				Case TransferState.BuildingDirectoryStructure
					' It informs us that the directory structure has been prepared for the multiple file transfer.
					_view.UpdateStatus("Building directory structure...")

'				#Region "Comparing File Events"

				Case TransferState.StartComparingFile
					' Source file and destination file are about to be compared.
					' To skip comparing these files, simply set e.Skip = true.
					' To override the comparison result, set the e.ComparionResult property.
					_view.UpdateStatus(String.Format("Comparing file {0}...", System.IO.Path.GetFileName(e.SourcePath)))
					_view.UpdateProgress(e, True, True)

				Case TransferState.Comparing
					' Source file and destination file are being compared.
					_view.UpdateStatus(GetSpeedInfo("Comparing", e))
					_view.UpdateProgress(e, True, True)

				Case TransferState.FileCompared
					' Source file and destination file have been compared.
					' Comparison result is saved in the e.ComparisonResult property.
					_view.UpdateProgress(e, True, True)

'				#End Region

'				#Region "Uploading File Events"

				Case TransferState.StartUploadingFile
					' Source file (local file) is about to be uploaded. Destination file is the remote file.
					' To skip uploading this file, simply set e.Skip = true.
					_view.UpdateStatus(String.Format("Uploading file {0}...", System.IO.Path.GetFileName(e.SourcePath)))
					_view.UpdateProgress(e, True, True)

				Case TransferState.Uploading
					' Source file is being uploaded to the remote server.
					_view.UpdateStatus(GetSpeedInfo("Uploading", e))
					_view.UpdateProgress(e, True, True)

				Case TransferState.FileUploaded
					' Source file has been uploaded.
					_view.UpdateProgress(e, True, True)

'				#End Region

'				#Region "Downloading File Events"

				Case TransferState.StartDownloadingFile
					' Source file (remote file) is about to be downloaded.
					' To skip uploading this file, simply set e.Skip = true.
					_view.UpdateStatus(String.Format("Downloading file {0}...", System.IO.Path.GetFileName(e.SourcePath)))
					_view.UpdateProgress(e, True, True)

				Case TransferState.Downloading
					' Source file is being downloaded to the local disk.
					_view.UpdateStatus(GetSpeedInfo("Downloading", e))
					_view.UpdateProgress(e, True, True)

				Case TransferState.FileDownloaded
					' Remote file has been downloaded.
					_view.UpdateProgress(e, True, True)

'				#End Region
			End Select
		End Sub

#If FTP Then
		''' <summary>
		''' Executes a command on the server.
		''' </summary>
		Public Function ExecCommand(ByVal command As String) As Boolean
				Try
					CType(_client, ComponentPro.Net.Ftp).SendCommand(command)
					CType(_client, ComponentPro.Net.Ftp).ReadResponse()

					If command.StartsWith("cwd ", StringComparison.InvariantCultureIgnoreCase) Then
						_currentDirectory = _client.GetCurrentDirectory()
						_view.UpdateRemotePath(_currentDirectory)
					End If
				Catch exc As Exception
					HandleException(exc)
					Return False
				End Try

			RefreshRemoteList()

			Return True
		End Function
#End If
	End Class
End Namespace
