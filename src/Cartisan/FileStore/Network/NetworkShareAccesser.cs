using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Cartisan.FileStore.Network {
    /// <summary>
    /// 网络共享访问连接器，用于FileStore连接CIFS/Samba/NAS
    /// </summary>
    public class NetworkShareAccesser {
        private readonly string _uncName;
        private readonly string _userName;
        private readonly string _password;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uncName">完整UNC路径</param>
        /// <param name="userName">访问共享连接的用户名</param>
        /// <param name="password">访问共享连接的密码</param>
        public NetworkShareAccesser(string uncName, string userName, string password) {
            _uncName = uncName;
            _userName = userName;
            _password = password;
        }

        /// <summary>
        /// 创建一个网络连接
        /// </summary>
        public void Connect() {
            int result = WNetAddConnection2(new NetResource() {
                Scope = ResourceScope.GlobalNetwork,
                ResourceType = ResourceType.Disk,
                DisplayType = ResourceDisplayType.Share,
                RemoteName = _uncName.TrimEnd('\\')
            }, _password, _uncName, 0);
            if (result!=0) {
                throw new Win32Exception(result);
            }
        }

        /// <summary>
        /// 释放一个网络连接
        /// </summary>
        public void Disconnect() {
            WNetCancelConnection2(_uncName, 1, true);
        }

        /// <summary>
        /// The WNetAddConnection2 function makes a connection to a network resource. 
        /// The function can redirect a local device to the network resource.
        /// </summary>
        /// <param name="netResource">A <see cref="T:Cartisan.FileStore.NetResource"/> 
        /// structure that specifies details of the proposed connection, such as information about the network resource, 
        /// the local device, and the network resource provider.
        /// </param>
        /// <param name="password">The password to use when connecting to the network resource.</param>
        /// <param name="username">The username to use when connecting to the network resource.</param>
        /// <param name="flags">The flags. See http://msdn.microsoft.com/en-us/library/aa385413%28VS.85%29.aspx for more information.</param>
        /// <returns/>
        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(NetResource netResource, string password, string username, int flags);

        /// <summary>
        /// The WNetCancelConnection2 function cancels an existing network connection. 
        /// You can also call the function to remove remembered network connections that are not currently connected.
        /// </summary>
        /// <param name="name">Specifies the name of either the redirected local device or the remote network resource to disconnect from.</param>
        /// <param name="flags">Connection type. The following values are defined:
        ///             0: The system does not update information about the connection. If the connection was marked as persistent in the registry, the system continues to restore the connection at the next logon. If the connection was not marked as persistent, the function ignores the setting of the CONNECT_UPDATE_PROFILE flag.
        ///             CONNECT_UPDATE_PROFILE: The system updates the user profile with the information that the connection is no longer a persistent one. The system will not restore this connection during subsequent logon operations. (Disconnecting resources using remote names has no effect on persistent connections.)
        /// </param>
        /// <param name="force">Specifies whether the disconnection should occur if there are open files or jobs on the connection. If this parameter is FALSE, the function fails if there are open files or jobs.</param>
        /// <returns/>
        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags, bool force);
    }
}