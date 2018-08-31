using System;
using System.Collections.Generic;
using System.Threading;

namespace ChatServer {

	public static class Pool {

		private const int threadQtd = 1000;

		private static List<Thread> threadList;
        private static Semaphore threadSync;

		private static Queue<Action> jobQueue;
		private static object jobQueueLock;
        
		static Pool() {

			threadList = new List<Thread>(threadQtd);
			threadSync = new Semaphore(0, threadQtd);

			jobQueue = new Queue<Action>();
			jobQueueLock = new object();

			for (var i = 0; i < threadQtd; i++) {

				var thread = new Thread(WorkerThread) { IsBackground = true };

				threadList.Add(thread);

				thread.Start();

			}

		}

		public static void RunJob(Action job) {

			lock (jobQueueLock) {

				jobQueue.Enqueue(job);

			}

			threadSync.Release();

		}

		private static void WorkerThread() {

			while (true) {

				threadSync.WaitOne();

				Action job;

				lock (jobQueueLock) {

					job = jobQueue.Dequeue();

				}

				try {

					job.Invoke();

				}catch(Exception ex){

					Console.WriteLine(ex);

				}

			}

		}

	}

}
