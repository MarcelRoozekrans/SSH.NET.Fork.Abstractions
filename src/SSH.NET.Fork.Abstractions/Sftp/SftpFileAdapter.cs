using Renci.SshNet.Sftp;
using System;

namespace SSH.NET.Fork.Abstractions.Sftp
{
    internal class SftpFileAdapter : ISftpFile
    {
        private readonly SftpFile _file;

        public SftpFileAdapter(SftpFile file)
        {
            _file = file;
        }

        /// <summary>Sets file  permissions.</summary>
        /// <param name="mode">The mode.</param>
        public void SetPermissions(short mode)
        {
            _file.SetPermissions(mode);
        }

        /// <summary>Permanently deletes a file on remote machine.</summary>
        public void Delete()
        {
            _file.Delete();
        }

        /// <summary>
        /// Moves a specified file to a new location on remote machine, providing the option to specify a new file name.
        /// </summary>
        /// <param name="destFileName">The path to move the file to, which can specify a different file name.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="destFileName" /> is <c>null</c>.</exception>
        public void MoveTo(string destFileName)
        {
            _file.MoveTo(destFileName);
        }

        /// <summary>Updates file status on the server.</summary>
        public void UpdateStatus()
        {
            _file.UpdateStatus();
        }

        /// <summary>Gets the file attributes.</summary>
        public SftpFileAttributes Attributes => _file.Attributes;

        /// <summary>Gets the full path of the directory or file.</summary>
        public string FullName => _file.FullName;

        /// <summary>
        /// For files, gets the name of the file. For directories, gets the name of the last directory in the hierarchy if a hierarchy exists.
        /// Otherwise, the Name property gets the name of the directory.
        /// </summary>
        public string Name => _file.Name;

        /// <summary>
        /// Gets or sets the time the current file or directory was last accessed.
        /// </summary>
        /// <value>
        /// The time that the current file or directory was last accessed.
        /// </value>
        public DateTime LastAccessTime
        {
            get => _file.LastAccessTime;
            set => _file.LastAccessTime = value;
        }

        /// <summary>
        /// Gets or sets the time when the current file or directory was last written to.
        /// </summary>
        /// <value>The time the current file was last written.</value>
        public DateTime LastWriteTime
        {
            get => _file.LastWriteTime;
            set => _file.LastWriteTime = value;
        }

        /// <summary>
        /// Gets or sets the time, in coordinated universal time (UTC), the current file or directory was last accessed.
        /// </summary>
        /// <value>
        /// The time that the current file or directory was last accessed.
        /// </value>
        public DateTime LastAccessTimeUtc
        {
            get => _file.LastAccessTimeUtc;
            set => _file.LastAccessTimeUtc = value;
        }

        /// <summary>
        /// Gets or sets the time, in coordinated universal time (UTC), when the current file or directory was last written to.
        /// </summary>
        /// <value>The time the current file was last written.</value>
        public DateTime LastWriteTimeUtc
        {
            get => _file.LastWriteTimeUtc;
            set => _file.LastWriteTimeUtc = value;
        }

        /// <summary>Gets or sets the size, in bytes, of the current file.</summary>
        /// <value>The size of the current file in bytes.</value>
        public long Length => _file.Length;

        /// <summary>Gets or sets file user id.</summary>
        /// <value>File user id.</value>
        public int UserId
        {
            get => _file.UserId;
            set => _file.UserId = value;
        }

        /// <summary>Gets or sets file group id.</summary>
        /// <value>File group id.</value>
        public int GroupId
        {
            get => _file.GroupId;
            set => _file.GroupId = value;
        }

        /// <summary>
        /// Gets a value indicating whether file represents a socket.
        /// </summary>
        /// <value>
        ///   <c>true</c> if file represents a socket; otherwise, <c>false</c>.
        /// </value>
        public bool IsSocket => _file.IsSocket;

        /// <summary>
        /// Gets a value indicating whether file represents a symbolic link.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if file represents a symbolic link; otherwise, <c>false</c>.
        /// </value>
        public bool IsSymbolicLink => _file.IsSymbolicLink;

        /// <summary>
        /// Gets a value indicating whether file represents a regular file.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if file represents a regular file; otherwise, <c>false</c>.
        /// </value>
        public bool IsRegularFile => _file.IsRegularFile;

        /// <summary>
        /// Gets a value indicating whether file represents a block device.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if file represents a block device; otherwise, <c>false</c>.
        /// </value>
        public bool IsBlockDevice => _file.IsBlockDevice;

        /// <summary>
        /// Gets a value indicating whether file represents a directory.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if file represents a directory; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirectory => _file.IsDirectory;

        /// <summary>
        /// Gets a value indicating whether file represents a character device.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if file represents a character device; otherwise, <c>false</c>.
        /// </value>
        public bool IsCharacterDevice => _file.IsCharacterDevice;

        /// <summary>
        /// Gets a value indicating whether file represents a named pipe.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if file represents a named pipe; otherwise, <c>false</c>.
        /// </value>
        public bool IsNamedPipe => _file.IsNamedPipe;

        /// <summary>
        /// Gets or sets a value indicating whether the owner can read from this file.
        /// </summary>
        /// <value>
        ///   <c>true</c> if owner can read from this file; otherwise, <c>false</c>.
        /// </value>
        public bool OwnerCanRead
        {
            get => _file.OwnerCanRead;
            set => _file.OwnerCanRead = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the owner can write into this file.
        /// </summary>
        /// <value>
        ///   <c>true</c> if owner can write into this file; otherwise, <c>false</c>.
        /// </value>
        public bool OwnerCanWrite
        {
            get => _file.OwnerCanWrite;
            set => _file.OwnerCanWrite = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the owner can execute this file.
        /// </summary>
        /// <value>
        ///   <c>true</c> if owner can execute this file; otherwise, <c>false</c>.
        /// </value>
        public bool OwnerCanExecute
        {
            get => _file.OwnerCanExecute;
            set => _file.OwnerCanExecute = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the group members can read from this file.
        /// </summary>
        /// <value>
        ///   <c>true</c> if group members can read from this file; otherwise, <c>false</c>.
        /// </value>
        public bool GroupCanRead
        {
            get => _file.GroupCanRead;
            set => _file.GroupCanRead = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the group members can write into this file.
        /// </summary>
        /// <value>
        ///   <c>true</c> if group members can write into this file; otherwise, <c>false</c>.
        /// </value>
        public bool GroupCanWrite
        {
            get => _file.GroupCanWrite;
            set => _file.GroupCanWrite = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the group members can execute this file.
        /// </summary>
        /// <value>
        ///   <c>true</c> if group members can execute this file; otherwise, <c>false</c>.
        /// </value>
        public bool GroupCanExecute
        {
            get => _file.GroupCanExecute;
            set => _file.GroupCanExecute = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the others can read from this file.
        /// </summary>
        /// <value>
        ///   <c>true</c> if others can read from this file; otherwise, <c>false</c>.
        /// </value>
        public bool OthersCanRead
        {
            get => _file.OthersCanRead;
            set => _file.OthersCanRead = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the others can write into this file.
        /// </summary>
        /// <value>
        ///   <c>true</c> if others can write into this file; otherwise, <c>false</c>.
        /// </value>
        public bool OthersCanWrite
        {
            get => _file.OthersCanWrite;
            set => _file.OthersCanWrite = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the others can execute this file.
        /// </summary>
        /// <value>
        ///   <c>true</c> if others can execute this file; otherwise, <c>false</c>.
        /// </value>
        public bool OthersCanExecute
        {
            get => _file.OthersCanExecute;
            set => _file.OthersCanExecute = value;
        }
    }
}
