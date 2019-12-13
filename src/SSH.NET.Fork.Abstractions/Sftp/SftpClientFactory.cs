using Renci.SshNet;

namespace SSH.NET.Fork.Abstractions.Sftp
{
    public class SftpClientFactory : ISftpClientFactory
    {
        public ISftpClient CreateClient(ConnectionInfo connectionInfo)
        {
            return new SftpClientAdapter(new SftpClient(connectionInfo));
        }

        public ISftpClient CreateClient(string host, int port, string username, params PrivateKeyFile[] keyFiles)
        {
            return new SftpClientAdapter(new SftpClient(host, port, username, keyFiles));
        }

        public ISftpClient CreateClient(string host, int port, string username, string password)
        {
            return new SftpClientAdapter(new SftpClient(host, port, username, password));
        }

        public ISftpClient CreateClient(string host, string username, params PrivateKeyFile[] keyFiles)
        {
            return new SftpClientAdapter(new SftpClient(host, username, keyFiles));
        }

        public ISftpClient CreateClient(string host, string username, string password)
        {
            return new SftpClientAdapter(new SftpClient(host, username, password));
        }
    }
}
