﻿using Cartisan.Extensions;

namespace Cartisan.Web.Authentication {
    public class User {
        public long UserId { get; set; }
        public string UserName { get; set; }

        public override string ToString() {
            return this.ToJson();
        }
    }
}