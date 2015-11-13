using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Cartisan.Website {
    public class SmsService: IIdentityMessageService {
        public Task SendAsync(IdentityMessage message) {
            // 在此处插入 SMS 服务可发送短信。
            return Task.FromResult(0);
        }
    }
}