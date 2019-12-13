using Renci.SshNet.Common;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Renci.SshNet;

namespace SSH.NET.Fork.Abstractions.Sftp
{
    internal class SftpClientAdapter : ISftpClient
    {
        private readonly SftpClient _client;

        public SftpClientAdapter(SftpClient client)
        {
            _client = client;
        }

        /// <summary>Gets sftp protocol version.</summary>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public int ProtocolVersion => _client.ProtocolVersion;

        /// <summary>Connects client to the server.</summary>
        /// <exception cref="T:System.InvalidOperationException">The client is already connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <exception cref="T:System.Net.Sockets.SocketException">Socket connection to the SSH server or proxy server could not be established, or an error occurred while resolving the hostname.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">SSH session could not be established.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshAuthenticationException">Authentication of SSH session failed.</exception>
        /// <exception cref="T:Renci.SshNet.Common.ProxyException">Failed to establish proxy connection.</exception>
        public void Connect()
        {
            _client.Connect();
        }

        /// <summary>Disconnects client from the server.</summary>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public void Disconnect()
        {
            _client.Disconnect();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _client.Dispose();
        }

        /// <summary>Gets the connection info.</summary>
        /// <value>The connection info.</value>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public ConnectionInfo ConnectionInfo => _client.ConnectionInfo;

        /// <summary>
        /// Gets a value indicating whether this client is connected to the server.
        /// </summary>
        /// <value>
        /// <c>true</c> if this client is connected; otherwise, <c>false</c>.
        /// </value>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public bool IsConnected => _client.IsConnected;

        /// <summary>Gets or sets the keep-alive interval.</summary>
        /// <value>
        /// The keep-alive interval. Specify negative one (-1) milliseconds to disable the
        /// keep-alive. This is the default value.
        /// </value>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public TimeSpan KeepAliveInterval
        {
            get => _client.KeepAliveInterval;
            set => _client.KeepAliveInterval = value;
        }

        /// <summary>Occurs when an error occurred.</summary>
        /// <example>
        ///   <code source="..\..\src\Renci.SshNet.Tests\Classes\SshClientTest.cs" region="Example SshClient Connect ErrorOccurred" language="C#" title="Handle ErrorOccurred event" />
        /// </example>
        public event EventHandler<ExceptionEventArgs> ErrorOccurred
        {
            add => _client.ErrorOccurred += value;
            remove => _client.ErrorOccurred -= value;
        }

        /// <summary>Occurs when host key received.</summary>
        /// <example>
        ///   <code source="..\..\src\Renci.SshNet.Tests\Classes\SshClientTest.cs" region="Example SshClient Connect HostKeyReceived" language="C#" title="Handle HostKeyReceived event" />
        /// </example>
        public event EventHandler<HostKeyEventArgs> HostKeyReceived
        {
            add => _client.HostKeyReceived += value;
            remove => _client.HostKeyReceived -= value;
        }

        /// <summary>Changes remote directory to path.</summary>
        /// <param name="path">New directory path.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to change directory denied by remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException"><paramref name="path" /> was not found on the remote host.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public void ChangeDirectory(string path)
        {
            _client.ChangeDirectory(path);
        }

        /// <summary>Changes permissions of file(s) to specified mode.</summary>
        /// <param name="path">File(s) path, may match multiple files.</param>
        /// <param name="mode">The mode.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to change permission on the path(s) was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException"><paramref name="path" /> was not found on the remote host.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public void ChangePermissions(string path, short mode)
        {
            _client.ChangePermissions(path, mode);
        }

        /// <summary>Creates remote directory specified by path.</summary>
        /// <param name="path">Directory path to create.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to create the directory was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public void CreateDirectory(string path)
        {
            _client.CreateDirectory(path);
        }

        /// <summary>Deletes remote directory specified by path.</summary>
        /// <param name="path">Directory to be deleted path.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException"><paramref name="path" /> was not found on the remote host.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to delete the directory was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public void DeleteDirectory(string path)
        {
            _client.DeleteDirectory(path);
        }

        /// <summary>Deletes remote file specified by path.</summary>
        /// <param name="path">File to be deleted path.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException"><paramref name="path" /> was not found on the remote host.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to delete the file was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public void DeleteFile(string path)
        {
            _client.DeleteFile(path);
        }

        /// <summary>Renames remote file from old path to new path.</summary>
        /// <param name="oldPath">Path to the old file location.</param>
        /// <param name="newPath">Path to the new file location.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="oldPath" /> is <b>null</b>. <para>-or-</para> or <paramref name="newPath" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to rename the file was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public void RenameFile(string oldPath, string newPath)
        {
            _client.RenameFile(oldPath, newPath);
        }

        /// <summary>Renames remote file from old path to new path.</summary>
        /// <param name="oldPath">Path to the old file location.</param>
        /// <param name="newPath">Path to the new file location.</param>
        /// <param name="isPosix">if set to <c>true</c> then perform a posix rename.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="oldPath" /> is <b>null</b>. <para>-or-</para> or <paramref name="newPath" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to rename the file was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public void RenameFile(string oldPath, string newPath, bool isPosix)
        {
            _client.RenameFile(oldPath, newPath, isPosix);
        }

        /// <summary>Creates a symbolic link from old path to new path.</summary>
        /// <param name="path">The old path.</param>
        /// <param name="linkPath">The new path.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b>. <para>-or-</para> <paramref name="linkPath" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to create the symbolic link was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public void SymbolicLink(string path, string linkPath)
        {
            _client.SymbolicLink(path, linkPath);
        }

        /// <summary>Retrieves list of files in remote directory.</summary>
        /// <param name="path">The path.</param>
        /// <param name="listCallback">The list callback.</param>
        /// <returns>A list of files.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to list the contents of the directory was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public IEnumerable<ISftpFile> ListDirectory(string path, Action<int> listCallback = null)
        {
            return _client.ListDirectory(path, listCallback)
                .Select(file => new SftpFileAdapter(file));
        }

        /// <summary>
        /// Begins an asynchronous operation of retrieving list of files in remote directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="asyncCallback">The method to be called when the asynchronous write operation is completed.</param>
        /// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
        /// <param name="listCallback">The list callback.</param>
        /// <returns>
        /// An <see cref="T:System.IAsyncResult" /> that references the asynchronous operation.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public IAsyncResult BeginListDirectory(string path, AsyncCallback asyncCallback, object state, Action<int> listCallback = null)
        {
            return _client.BeginListDirectory(path, asyncCallback, state, listCallback);
        }

        /// <summary>
        /// Ends an asynchronous operation of retrieving list of files in remote directory.
        /// </summary>
        /// <param name="asyncResult">The pending asynchronous SFTP request.</param>
        /// <returns>A list of files.</returns>
        /// <exception cref="T:System.ArgumentException">The <see cref="T:System.IAsyncResult" /> object did not come from the corresponding async method on this type.<para>-or-</para><see cref="M:Renci.SshNet.SftpClient.EndListDirectory(System.IAsyncResult)" /> was called multiple times with the same <see cref="T:System.IAsyncResult" />.</exception>
        public IEnumerable<ISftpFile> EndListDirectory(IAsyncResult asyncResult)
        {
            return _client.EndListDirectory(asyncResult)
                .Select(file => new SftpFileAdapter(file));
        }

        /// <summary>Gets reference to remote file or directory.</summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// A reference to <see cref="T:Renci.SshNet.Sftp.SftpFile" /> file object.
        /// </returns>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException"><paramref name="path" /> was not found on the remote host.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public ISftpFile Get(string path)
        {
            return new SftpFileAdapter(_client.Get(path));
        }

        /// <summary>Checks whether file or directory exists;</summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// <c>true</c> if directory or file exists; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to perform the operation was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public bool Exists(string path)
        {
            return _client.Exists(path);
        }

        /// <summary>
        /// Downloads remote file specified by the path into the stream.
        /// </summary>
        /// <param name="path">File to download.</param>
        /// <param name="output">Stream to write the file into.</param>
        /// <param name="downloadCallback">The download callback.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="output" /> is <b>null</b>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to perform the operation was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException"><paramref name="path" /> was not found on the remote host.</exception>
        /// /// 
        ///             <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// Method calls made by this method to <paramref name="output" />, may under certain conditions result in exceptions thrown by the stream.
        /// </remarks>
        public void DownloadFile(string path, Stream output, Action<ulong> downloadCallback = null)
        {
            _client.DownloadFile(path, output, downloadCallback);
        }

        /// <summary>
        /// Begins an asynchronous file downloading into the stream.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="output">The output.</param>
        /// <returns>
        /// An <see cref="T:System.IAsyncResult" /> that references the asynchronous operation.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="output" /> is <b>null</b>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to perform the operation was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// Method calls made by this method to <paramref name="output" />, may under certain conditions result in exceptions thrown by the stream.
        /// </remarks>
        public IAsyncResult BeginDownloadFile(string path, Stream output)
        {
            return _client.BeginDownloadFile(path, output);
        }

        /// <summary>
        /// Begins an asynchronous file downloading into the stream.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="output">The output.</param>
        /// <param name="asyncCallback">The method to be called when the asynchronous write operation is completed.</param>
        /// <returns>
        /// An <see cref="T:System.IAsyncResult" /> that references the asynchronous operation.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="output" /> is <b>null</b>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to perform the operation was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// Method calls made by this method to <paramref name="output" />, may under certain conditions result in exceptions thrown by the stream.
        /// </remarks>
        public IAsyncResult BeginDownloadFile(string path, Stream output, AsyncCallback asyncCallback)
        {
            return _client.BeginDownloadFile(path, output, asyncCallback);
        }

        /// <summary>
        /// Begins an asynchronous file downloading into the stream.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="output">The output.</param>
        /// <param name="asyncCallback">The method to be called when the asynchronous write operation is completed.</param>
        /// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
        /// <param name="downloadCallback">The download callback.</param>
        /// <returns>
        /// An <see cref="T:System.IAsyncResult" /> that references the asynchronous operation.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="output" /> is <b>null</b>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// Method calls made by this method to <paramref name="output" />, may under certain conditions result in exceptions thrown by the stream.
        /// </remarks>
        public IAsyncResult BeginDownloadFile(string path, Stream output, AsyncCallback asyncCallback, object state,
            Action<ulong> downloadCallback = null)
        {
            return _client.BeginDownloadFile(path, output, asyncCallback, state, downloadCallback);
        }

        /// <summary>
        /// Ends an asynchronous file downloading into the stream.
        /// </summary>
        /// <param name="asyncResult">The pending asynchronous SFTP request.</param>
        /// <exception cref="T:System.ArgumentException">The <see cref="T:System.IAsyncResult" /> object did not come from the corresponding async method on this type.<para>-or-</para><see cref="M:Renci.SshNet.SftpClient.EndDownloadFile(System.IAsyncResult)" /> was called multiple times with the same <see cref="T:System.IAsyncResult" />.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to perform the operation was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The path was not found on the remote host.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        public void EndDownloadFile(IAsyncResult asyncResult)
        {
            _client.EndDownloadFile(asyncResult);
        }

        /// <summary>Uploads stream into remote file.</summary>
        /// <param name="input">Data input stream.</param>
        /// <param name="path">Remote file path.</param>
        /// <param name="uploadCallback">The upload callback.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="input" /> is <b>null</b>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to upload the file was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// Method calls made by this method to <paramref name="input" />, may under certain conditions result in exceptions thrown by the stream.
        /// </remarks>
        public void UploadFile(Stream input, string path, Action<ulong> uploadCallback = null)
        {
            _client.UploadFile(input, path, uploadCallback);
        }

        /// <summary>Uploads stream into remote file.</summary>
        /// <param name="input">Data input stream.</param>
        /// <param name="path">Remote file path.</param>
        /// <param name="canOverride">if set to <c>true</c> then existing file will be overwritten.</param>
        /// <param name="uploadCallback">The upload callback.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="input" /> is <b>null</b>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to upload the file was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// Method calls made by this method to <paramref name="input" />, may under certain conditions result in exceptions thrown by the stream.
        /// </remarks>
        public void UploadFile(Stream input, string path, bool canOverride, Action<ulong> uploadCallback = null)
        {
            _client.UploadFile(input, path, canOverride, uploadCallback);
        }

        /// <summary>
        /// Begins an asynchronous uploading the stream into remote file.
        /// </summary>
        /// <param name="input">Data input stream.</param>
        /// <param name="path">Remote file path.</param>
        /// <returns>
        /// An <see cref="T:System.IAsyncResult" /> that references the asynchronous operation.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="input" /> is <b>null</b>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to list the contents of the directory was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// <para>
        /// Method calls made by this method to <paramref name="input" />, may under certain conditions result in exceptions thrown by the stream.
        /// </para>
        /// <para>
        /// If the remote file already exists, it is overwritten and truncated.
        /// </para>
        /// </remarks>
        public IAsyncResult BeginUploadFile(Stream input, string path)
        {
            return _client.BeginUploadFile(input, path);
        }

        /// <summary>
        /// Begins an asynchronous uploading the stream into remote file.
        /// </summary>
        /// <param name="input">Data input stream.</param>
        /// <param name="path">Remote file path.</param>
        /// <param name="asyncCallback">The method to be called when the asynchronous write operation is completed.</param>
        /// <returns>
        /// An <see cref="T:System.IAsyncResult" /> that references the asynchronous operation.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="input" /> is <b>null</b>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to list the contents of the directory was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// <para>
        /// Method calls made by this method to <paramref name="input" />, may under certain conditions result in exceptions thrown by the stream.
        /// </para>
        /// <para>
        /// If the remote file already exists, it is overwritten and truncated.
        /// </para>
        /// </remarks>
        public IAsyncResult BeginUploadFile(Stream input, string path, AsyncCallback asyncCallback)
        {
            return _client.BeginUploadFile(input, path, asyncCallback);
        }

        /// <summary>
        /// Begins an asynchronous uploading the stream into remote file.
        /// </summary>
        /// <param name="input">Data input stream.</param>
        /// <param name="path">Remote file path.</param>
        /// <param name="asyncCallback">The method to be called when the asynchronous write operation is completed.</param>
        /// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
        /// <param name="uploadCallback">The upload callback.</param>
        /// <returns>
        /// An <see cref="T:System.IAsyncResult" /> that references the asynchronous operation.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="input" /> is <b>null</b>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to list the contents of the directory was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// <para>
        /// Method calls made by this method to <paramref name="input" />, may under certain conditions result in exceptions thrown by the stream.
        /// </para>
        /// <para>
        /// If the remote file already exists, it is overwritten and truncated.
        /// </para>
        /// </remarks>
        public IAsyncResult BeginUploadFile(Stream input, string path, AsyncCallback asyncCallback, object state,
            Action<ulong> uploadCallback = null)
        {
            return _client.BeginUploadFile(input, path, asyncCallback, state, uploadCallback);
        }

        /// <summary>
        /// Begins an asynchronous uploading the stream into remote file.
        /// </summary>
        /// <param name="input">Data input stream.</param>
        /// <param name="path">Remote file path.</param>
        /// <param name="canOverride">Specified whether an existing file can be overwritten.</param>
        /// <param name="asyncCallback">The method to be called when the asynchronous write operation is completed.</param>
        /// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
        /// <param name="uploadCallback">The upload callback.</param>
        /// <returns>
        /// An <see cref="T:System.IAsyncResult" /> that references the asynchronous operation.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="input" /> is <b>null</b>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <b>null</b> or contains only whitespace characters.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// <para>
        /// Method calls made by this method to <paramref name="input" />, may under certain conditions result in exceptions thrown by the stream.
        /// </para>
        /// <para>
        /// When <paramref name="path" /> refers to an existing file, set <paramref name="canOverride" /> to <c>true</c> to overwrite and truncate that file.
        /// If <paramref name="canOverride" /> is <c>false</c>, the upload will fail and <see cref="M:Renci.SshNet.SftpClient.EndUploadFile(System.IAsyncResult)" /> will throw an
        /// <see cref="T:Renci.SshNet.Common.SshException" />.
        /// </para>
        /// </remarks>
        public IAsyncResult BeginUploadFile(Stream input, string path, bool canOverride, AsyncCallback asyncCallback, object state,
            Action<ulong> uploadCallback = null)
        {
            return _client.BeginUploadFile(input, path, canOverride, asyncCallback, state, uploadCallback);
        }

        /// <summary>
        /// Ends an asynchronous uploading the stream into remote file.
        /// </summary>
        /// <param name="asyncResult">The pending asynchronous SFTP request.</param>
        /// <exception cref="T:System.ArgumentException">The <see cref="T:System.IAsyncResult" /> object did not come from the corresponding async method on this type.<para>-or-</para><see cref="M:Renci.SshNet.SftpClient.EndUploadFile(System.IAsyncResult)" /> was called multiple times with the same <see cref="T:System.IAsyncResult" />.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The directory of the file was not found on the remote host.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPermissionDeniedException">Permission to upload the file was denied by the remote host. <para>-or-</para> A SSH command was denied by the server.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshException">A SSH error where <see cref="P:System.Exception.Message" /> is the message from the remote host.</exception>
        public void EndUploadFile(IAsyncResult asyncResult)
        {
            _client.EndUploadFile(asyncResult);
        }

        /// <summary>Gets status using statvfs@openssh.com request.</summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// A <see cref="T:Renci.SshNet.Sftp.SftpFileSytemInformation" /> instance that contains file status information.
        /// </returns>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public SftpFileSytemInformation GetStatus(string path)
        {
            return _client.GetStatus(path);
        }

        /// <summary>
        /// Appends lines to a file, creating the file if it does not already exist.
        /// </summary>
        /// <param name="path">The file to append the lines to. The file is created if it does not already exist.</param>
        /// <param name="contents">The lines to append to the file.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is<b>null</b> <para>-or-</para> <paramref name="contents" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// The characters are written to the file using UTF-8 encoding without a Byte-Order Mark (BOM)
        /// </remarks>
        public void AppendAllLines(string path, IEnumerable<string> contents)
        {
            _client.AppendAllLines(path, contents);
        }

        /// <summary>
        /// Appends lines to a file by using a specified encoding, creating the file if it does not already exist.
        /// </summary>
        /// <param name="path">The file to append the lines to. The file is created if it does not already exist.</param>
        /// <param name="contents">The lines to append to the file.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>. <para>-or-</para> <paramref name="contents" /> is <b>null</b>. <para>-or-</para> <paramref name="encoding" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            _client.AppendAllLines(path, contents, encoding);
        }

        /// <summary>
        /// Appends the specified string to the file, creating the file if it does not already exist.
        /// </summary>
        /// <param name="path">The file to append the specified string to.</param>
        /// <param name="contents">The string to append to the file.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>. <para>-or-</para> <paramref name="contents" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// The characters are written to the file using UTF-8 encoding without a Byte-Order Mark (BOM).
        /// </remarks>
        public void AppendAllText(string path, string contents)
        {
            _client.AppendAllText(path, contents);
        }

        /// <summary>
        /// Appends the specified string to the file, creating the file if it does not already exist.
        /// </summary>
        /// <param name="path">The file to append the specified string to.</param>
        /// <param name="contents">The string to append to the file.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>. <para>-or-</para> <paramref name="contents" /> is <b>null</b>. <para>-or-</para> <paramref name="encoding" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public void AppendAllText(string path, string contents, Encoding encoding)
        {
            _client.AppendAllText(path, contents, encoding);
        }

        /// <summary>
        /// Creates a <see cref="T:System.IO.StreamWriter" /> that appends UTF-8 encoded text to the specified file,
        /// creating the file if it does not already exist.
        /// </summary>
        /// <param name="path">The path to the file to append to.</param>
        /// <returns>
        /// A <see cref="T:System.IO.StreamWriter" /> that appends text to a file using UTF-8 encoding without a
        /// Byte-Order Mark (BOM).
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public StreamWriter AppendText(string path)
        {
            return _client.AppendText(path);
        }

        /// <summary>
        /// Creates a <see cref="T:System.IO.StreamWriter" /> that appends text to a file using the specified
        /// encoding, creating the file if it does not already exist.
        /// </summary>
        /// <param name="path">The path to the file to append to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <returns>
        /// A <see cref="T:System.IO.StreamWriter" /> that appends text to a file using the specified encoding.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>. <para>-or-</para> <paramref name="encoding" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public StreamWriter AppendText(string path, Encoding encoding)
        {
            return _client.AppendText(path, encoding);
        }

        /// <summary>Creates or overwrites a file in the specified path.</summary>
        /// <param name="path">The path and name of the file to create.</param>
        /// <returns>
        /// A <see cref="T:Renci.SshNet.Sftp.SftpFileStream" /> that provides read/write access to the file specified in path.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// If the target file already exists, it is first truncated to zero bytes.
        /// </remarks>
        public Stream Create(string path)
        {
            return _client.Create(path);
        }

        /// <summary>Creates or overwrites the specified file.</summary>
        /// <param name="path">The path and name of the file to create.</param>
        /// <param name="bufferSize">The maximum number of bytes buffered for reads and writes to the file.</param>
        /// <returns>
        /// A <see cref="T:Renci.SshNet.Sftp.SftpFileStream" /> that provides read/write access to the file specified in path.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// If the target file already exists, it is first truncated to zero bytes.
        /// </remarks>
        public Stream Create(string path, int bufferSize)
        {
            return _client.Create(path, bufferSize);
        }

        /// <summary>
        /// Creates or opens a file for writing UTF-8 encoded text.
        /// </summary>
        /// <param name="path">The file to be opened for writing.</param>
        /// <returns>
        /// A <see cref="T:System.IO.StreamWriter" /> that writes text to a file using UTF-8 encoding without
        /// a Byte-Order Mark (BOM).
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// <para>
        /// If the target file already exists, it is overwritten. It is not first truncated to zero bytes.
        /// </para>
        /// <para>
        /// If the target file does not exist, it is created.
        /// </para>
        /// </remarks>
        public StreamWriter CreateText(string path)
        {
            return _client.CreateText(path);
        }

        /// <summary>
        /// Creates or opens a file for writing text using the specified encoding.
        /// </summary>
        /// <param name="path">The file to be opened for writing.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <returns>
        /// A <see cref="T:System.IO.StreamWriter" /> that writes to a file using the specified encoding.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// <para>
        /// If the target file already exists, it is overwritten. It is not first truncated to zero bytes.
        /// </para>
        /// <para>
        /// If the target file does not exist, it is created.
        /// </para>
        /// </remarks>
        public StreamWriter CreateText(string path, Encoding encoding)
        {
            return _client.CreateText(path, encoding);
        }

        /// <summary>Deletes the specified file or directory.</summary>
        /// <param name="path">The name of the file or directory to be deleted. Wildcard characters are not supported.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException"><paramref name="path" /> was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public void Delete(string path)
        {
            _client.Delete(path);
        }

        /// <summary>
        /// Returns the date and time the specified file or directory was last accessed.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain access date and time information.</param>
        /// <returns>
        /// A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last accessed.
        /// This value is expressed in local time.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public DateTime GetLastAccessTime(string path)
        {
            return _client.GetLastAccessTime(path);
        }

        /// <summary>
        /// Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last accessed.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain access date and time information.</param>
        /// <returns>
        /// A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last accessed.
        /// This value is expressed in UTC time.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public DateTime GetLastAccessTimeUtc(string path)
        {
            return _client.GetLastAccessTimeUtc(path);
        }

        /// <summary>
        /// Returns the date and time the specified file or directory was last written to.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain write date and time information.</param>
        /// <returns>
        /// A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last written to.
        /// This value is expressed in local time.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public DateTime GetLastWriteTime(string path)
        {
            return _client.GetLastWriteTime(path);
        }

        /// <summary>
        /// Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last written to.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain write date and time information.</param>
        /// <returns>
        /// A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last written to.
        /// This value is expressed in UTC time.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public DateTime GetLastWriteTimeUtc(string path)
        {
            return _client.GetLastWriteTimeUtc(path);
        }

        /// <summary>
        /// Opens a <see cref="T:Renci.SshNet.Sftp.SftpFileStream" /> on the specified path with read/write access.
        /// </summary>
        /// <param name="path">The file to open.</param>
        /// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten.</param>
        /// <returns>
        /// An unshared <see cref="T:Renci.SshNet.Sftp.SftpFileStream" /> that provides access to the specified file, with the specified mode and read/write access.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public Stream Open(string path, FileMode mode)
        {
            return _client.Open(path, mode);
        }

        /// <summary>
        /// Opens a <see cref="T:Renci.SshNet.Sftp.SftpFileStream" /> on the specified path, with the specified mode and access.
        /// </summary>
        /// <param name="path">The file to open.</param>
        /// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten.</param>
        /// <param name="access">A <see cref="T:System.IO.FileAccess" /> value that specifies the operations that can be performed on the file.</param>
        /// <returns>
        /// An unshared <see cref="T:Renci.SshNet.Sftp.SftpFileStream" /> that provides access to the specified file, with the specified mode and access.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public Stream Open(string path, FileMode mode, FileAccess access)
        {
            return _client.Open(path, mode, access);
        }

        /// <summary>Opens an existing file for reading.</summary>
        /// <param name="path">The file to be opened for reading.</param>
        /// <returns>
        /// A read-only <see cref="T:Renci.SshNet.Sftp.SftpFileStream" /> on the specified path.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public Stream OpenRead(string path)
        {
            return _client.OpenRead(path);
        }

        /// <summary>
        /// Opens an existing UTF-8 encoded text file for reading.
        /// </summary>
        /// <param name="path">The file to be opened for reading.</param>
        /// <returns>
        /// A <see cref="T:System.IO.StreamReader" /> on the specified path.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public StreamReader OpenText(string path)
        {
            return _client.OpenText(path);
        }

        /// <summary>Opens a file for writing.</summary>
        /// <param name="path">The file to be opened for writing.</param>
        /// <returns>
        /// An unshared <see cref="T:Renci.SshNet.Sftp.SftpFileStream" /> object on the specified path with <see cref="F:System.IO.FileAccess.Write" /> access.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>If the file does not exist, it is created.</remarks>
        public Stream OpenWrite(string path)
        {
            return _client.OpenWrite(path);
        }

        /// <summary>
        /// Opens a binary file, reads the contents of the file into a byte array, and closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        /// <returns>A byte array containing the contents of the file.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public byte[] ReadAllBytes(string path)
        {
            return _client.ReadAllBytes(path);
        }

        /// <summary>
        /// Opens a text file, reads all lines of the file using UTF-8 encoding, and closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        /// <returns>A string array containing all lines of the file.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public string[] ReadAllLines(string path)
        {
            return _client.ReadAllLines(path);
        }

        /// <summary>
        /// Opens a file, reads all lines of the file with the specified encoding, and closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        /// <param name="encoding">The encoding applied to the contents of the file.</param>
        /// <returns>A string array containing all lines of the file.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public string[] ReadAllLines(string path, Encoding encoding)
        {
            return _client.ReadAllLines(path, encoding);
        }

        /// <summary>
        /// Opens a text file, reads all lines of the file with the UTF-8 encoding, and closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        /// <returns>A string containing all lines of the file.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public string ReadAllText(string path)
        {
            return _client.ReadAllText(path);
        }

        /// <summary>
        /// Opens a file, reads all lines of the file with the specified encoding, and closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        /// <param name="encoding">The encoding applied to the contents of the file.</param>
        /// <returns>A string containing all lines of the file.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public string ReadAllText(string path, Encoding encoding)
        {
            return _client.ReadAllText(path, encoding);
        }

        /// <summary>Reads the lines of a file with the UTF-8 encoding.</summary>
        /// <param name="path">The file to read.</param>
        /// <returns>The lines of the file.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public IEnumerable<string> ReadLines(string path)
        {
            return _client.ReadLines(path);
        }

        /// <summary>
        /// Read the lines of a file that has a specified encoding.
        /// </summary>
        /// <param name="path">The file to read.</param>
        /// <param name="encoding">The encoding that is applied to the contents of the file.</param>
        /// <returns>The lines of the file.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public IEnumerable<string> ReadLines(string path, Encoding encoding)
        {
            return _client.ReadLines(path, encoding);
        }

        /// <summary>
        /// Writes the specified byte array to the specified file, and closes the file.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="bytes">The bytes to write to the file.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// <para>
        /// If the target file already exists, it is overwritten. It is not first truncated to zero bytes.
        /// </para>
        /// <para>
        /// If the target file does not exist, it is created.
        /// </para>
        /// </remarks>
        public void WriteAllBytes(string path, byte[] bytes)
        {
            _client.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// Writes a collection of strings to the file using the UTF-8 encoding, and closes the file.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The lines to write to the file.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// <para>
        /// The characters are written to the file using UTF-8 encoding without a Byte-Order Mark (BOM).
        /// </para>
        /// <para>
        /// If the target file already exists, it is overwritten. It is not first truncated to zero bytes.
        /// </para>
        /// <para>
        /// If the target file does not exist, it is created.
        /// </para>
        /// </remarks>
        public void WriteAllLines(string path, IEnumerable<string> contents)
        {
            _client.WriteAllLines(path, contents);
        }

        /// <summary>
        /// Write the specified string array to the file using the UTF-8 encoding, and closes the file.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The string array to write to the file.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// <para>
        /// The characters are written to the file using UTF-8 encoding without a Byte-Order Mark (BOM).
        /// </para>
        /// <para>
        /// If the target file already exists, it is overwritten. It is not first truncated to zero bytes.
        /// </para>
        /// <para>
        /// If the target file does not exist, it is created.
        /// </para>
        /// </remarks>
        public void WriteAllLines(string path, string[] contents)
        {
            _client.WriteAllLines(path, contents);
        }

        /// <summary>
        /// Writes a collection of strings to the file using the specified encoding, and closes the file.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The lines to write to the file.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// <para>
        /// If the target file already exists, it is overwritten. It is not first truncated to zero bytes.
        /// </para>
        /// <para>
        /// If the target file does not exist, it is created.
        /// </para>
        /// </remarks>
        public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            _client.WriteAllLines(path, contents, encoding);
        }

        /// <summary>
        /// Writes the specified string array to the file by using the specified encoding, and closes the file.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The string array to write to the file.</param>
        /// <param name="encoding">An <see cref="T:System.Text.Encoding" /> object that represents the character encoding applied to the string array.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// <para>
        /// If the target file already exists, it is overwritten. It is not first truncated to zero bytes.
        /// </para>
        /// <para>
        /// If the target file does not exist, it is created.
        /// </para>
        /// </remarks>
        public void WriteAllLines(string path, string[] contents, Encoding encoding)
        {
            _client.WriteAllLines(path, contents, encoding);
        }

        /// <summary>
        /// Writes the specified string to the file using the UTF-8 encoding, and closes the file.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The string to write to the file.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// <para>
        /// The characters are written to the file using UTF-8 encoding without a Byte-Order Mark (BOM).
        /// </para>
        /// <para>
        /// If the target file already exists, it is overwritten. It is not first truncated to zero bytes.
        /// </para>
        /// <para>
        /// If the target file does not exist, it is created.
        /// </para>
        /// </remarks>
        public void WriteAllText(string path, string contents)
        {
            _client.WriteAllText(path, contents);
        }

        /// <summary>
        /// Writes the specified string to the file using the specified encoding, and closes the file.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The string to write to the file.</param>
        /// <param name="encoding">The encoding to apply to the string.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The specified path is invalid, or its directory was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        /// <remarks>
        /// <para>
        /// If the target file already exists, it is overwritten. It is not first truncated to zero bytes.
        /// </para>
        /// <para>
        /// If the target file does not exist, it is created.
        /// </para>
        /// </remarks>
        public void WriteAllText(string path, string contents, Encoding encoding)
        {
            _client.WriteAllText(path, contents, encoding);
        }

        /// <summary>
        /// Gets the <see cref="T:Renci.SshNet.Sftp.SftpFileAttributes" /> of the file on the path.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>
        /// The <see cref="T:Renci.SshNet.Sftp.SftpFileAttributes" /> of the file on the path.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException"><paramref name="path" /> was not found on the remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public SftpFileAttributes GetAttributes(string path)
        {
            return _client.GetAttributes(path);
        }

        /// <summary>
        /// Sets the specified <see cref="T:Renci.SshNet.Sftp.SftpFileAttributes" /> of the file on the specified path.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="fileAttributes">The desired <see cref="T:Renci.SshNet.Sftp.SftpFileAttributes" />.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is <b>null</b>.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public void SetAttributes(string path, SftpFileAttributes fileAttributes)
        {
            _client.SetAttributes(path, fileAttributes);
        }

        /// <summary>Synchronizes the directories.</summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns>A list of uploaded files.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="sourcePath" /> is <c>null</c>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="destinationPath" /> is <c>null</c> or contains only whitespace.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException"><paramref name="destinationPath" /> was not found on the remote host.</exception>
        public IEnumerable<FileInfo> SynchronizeDirectories(string sourcePath, string destinationPath, string searchPattern)
        {
            return _client.SynchronizeDirectories(sourcePath, destinationPath, searchPattern);
        }

        /// <summary>Begins the synchronize directories.</summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="asyncCallback">The async callback.</param>
        /// <param name="state">The state.</param>
        /// <returns>
        /// An <see cref="T:System.IAsyncResult" /> that represents the asynchronous directory synchronization.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="sourcePath" /> is <c>null</c>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="destinationPath" /> is <c>null</c> or contains only whitespace.</exception>
        public IAsyncResult BeginSynchronizeDirectories(string sourcePath, string destinationPath, string searchPattern,
            AsyncCallback asyncCallback, object state)
        {
            return _client.BeginSynchronizeDirectories(sourcePath, destinationPath, searchPattern, asyncCallback, state);
        }

        /// <summary>Ends the synchronize directories.</summary>
        /// <param name="asyncResult">The async result.</param>
        /// <returns>A list of uploaded files.</returns>
        /// <exception cref="T:System.ArgumentException">The <see cref="T:System.IAsyncResult" /> object did not come from the corresponding async method on this type.<para>-or-</para><see cref="M:Renci.SshNet.SftpClient.EndSynchronizeDirectories(System.IAsyncResult)" /> was called multiple times with the same <see cref="T:System.IAsyncResult" />.</exception>
        /// <exception cref="T:Renci.SshNet.Common.SftpPathNotFoundException">The destination path was not found on the remote host.</exception>
        public IEnumerable<FileInfo> EndSynchronizeDirectories(IAsyncResult asyncResult)
        {
            return _client.EndSynchronizeDirectories(asyncResult);
        }

        /// <summary>Gets or sets the operation timeout.</summary>
        /// <value>
        /// The timeout to wait until an operation completes. The default value is negative
        /// one (-1) milliseconds, which indicates an infinite timeout period.
        /// </value>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public TimeSpan OperationTimeout
        {
            get => _client.OperationTimeout;
            set => _client.OperationTimeout = value;
        }

        /// <summary>Gets or sets the maximum size of the buffer in bytes.</summary>
        /// <value>
        /// The size of the buffer. The default buffer size is 32768 bytes (32 KB).
        /// </value>
        /// <remarks>
        /// <para>
        /// For write operations, this limits the size of the payload for
        /// individual SSH_FXP_WRITE messages. The actual size is always
        /// capped at the maximum packet size supported by the peer
        /// (minus the size of protocol fields).
        /// </para>
        /// <para>
        /// For read operations, this controls the size of the payload which
        /// is requested from the peer in a SSH_FXP_READ message. The peer
        /// will send the requested number of bytes in a SSH_FXP_DATA message,
        /// possibly split over multiple SSH_MSG_CHANNEL_DATA messages.
        /// </para>
        /// <para>
        /// To optimize the size of the SSH packets sent by the peer,
        /// the actual requested size will take into account the size of the
        /// SSH_FXP_DATA protocol fields.
        /// </para>
        /// <para>
        /// The size of the each indivual SSH_FXP_DATA message is limited to the
        /// local maximum packet size of the channel, which is set to <c>64 KB</c>
        /// for SSH.NET. However, the peer can limit this even further.
        /// </para>
        /// </remarks>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public uint BufferSize
        {
            get => _client.BufferSize;
            set => _client.BufferSize = value;
        }

        /// <summary>Gets remote working directory.</summary>
        /// <exception cref="T:Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The method was called after the client was disposed.</exception>
        public string WorkingDirectory => _client.WorkingDirectory;
    }
}
