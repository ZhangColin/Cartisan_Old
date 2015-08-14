namespace Cartisan.Configuration {
    /// <summary>
    /// 代表一个设置
    /// </summary>
    public class Setting {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public Setting(string name, string value) {
            this.Name = name;
            this.Value = value;
        }

        public override string ToString() {
            return Name;
        }
    }
}