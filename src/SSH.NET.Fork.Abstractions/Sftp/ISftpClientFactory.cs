using Renci.SshNet;

namespace SSH.NET.Fork.Abstractions.Sftp
{
    public interface ISftpClientFactory
    {
        ISftpClient CreateClient(ConnectionInfo connectionInfo);
        ISftpClient CreateClient(string host, int port, string username, params PrivateKeyFile[] keyFiles);
        ISftpClient CreateClient(string host, int port, string username, string password);
        ISftpClient CreateClient(string host, string username, params PrivateKeyFile[] keyFiles);
        ISftpClient CreateClient(string host, string username, string password);
    }
}
