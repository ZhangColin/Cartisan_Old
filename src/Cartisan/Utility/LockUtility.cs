using System;
using System.Collections;

namespace Cartisan.Utility {
    public class LockUtility {
        class LockObject {
            private volatile int _counter;

            public int Counter {
                get {
                    return _counter;
                }
            }

            internal void Decrement() {
                _counter--;
            }

            internal void Increate() {
                _counter++;
            }
        }

        private static readonly Hashtable _lockPool = new Hashtable();

        private static void ReleaseLock(object key, LockObject lockObject) {
            lock(_lockPool) {
                lockObject.Decrement();
                if(lockObject.Counter == 0) {
                    _lockPool.Remove(key);
                }
            }
        }

        private static LockObject GetLock(object key) {
            lock(_lockPool) {
                var lockObj = _lockPool[key] as LockObject;
                if(lockObj == null) {
                    lockObj = new LockObject();
                    _lockPool[key] = lockObj;
                }
                lockObj.Increate();

                return lockObj;
            }
        }

        public static void Lock(object key, Action action) {
            var lockObj = GetLock(key);

            try {
                lock(lockObj) {
                    action();
                }
            }
            finally {
                ReleaseLock(key, lockObj);
            }
        }
    }
}