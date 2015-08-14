using System;

namespace Cartisan.Tasks {
    /// <summary>
    /// 任务信息
    /// </summary>
    public class TaskInfo {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 任务的执行时间规则
        /// </summary>
        public string TaskRule { get; set; }

        /// <summary>
        /// 程序重启后立即执行
        /// </summary>
        /// <remarks>
        /// 在应用程序池重启后，是否检查此任务上次被执行过，如果没有执行则立即执行
        /// </remarks>
        public bool RunAtRestart { get; set; }

        /// <summary>
        /// 任务在哪台服务器上运行
        /// </summary>
        public RunAtServer RunAtServer { get; set; }

        /// <summary>
        /// 用于任务实例化
        /// </summary>
        public string ClassType { get; set; }

        /// <summary>
        /// 上次执行开始时间
        /// </summary>
        public DateTime? LastStart { get; set; }

        /// <summary>
        /// 上次执行结束时间
        /// </summary>
        public DateTime? LastEnd { get; set; }

        /// <summary>
        /// 上次任务执行状态
        /// </summary>
        /// <remarks>true-成功/false-失败</remarks>
        public bool? LastIsSuccess { get; set; }

        /// <summary>
        /// 下次执行时间
        /// </summary>
        public DateTime? NextStart { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 是否在运行
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// 获取规则指定部分
        /// </summary>
        /// <param name="rulePart">规则组成部分</param>
        /// <returns></returns>
        public string GetRulePart(RulePart rulePart) {
            if (string.IsNullOrEmpty(TaskRule)) {
                if (RulePart.DayOfWeek != rulePart) {
                    return "1";
                }
                return null;
            }

            string part = TaskRule.Split(' ').GetValue((int)rulePart).ToString();
            if (part == "*" || part == "?") {
                if (RulePart.DayOfWeek != rulePart) {
                    return "1";
                }
                return null;
            }
            if (part.Contains("/")) {
                return part.Substring(part.IndexOf("/") + 1);
            }

            return part;
        }
    }
}