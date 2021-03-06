namespace WpfMutexPractice
{
    using System.Threading;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// ミューテックス（二重起動防止）作成
        /// </summary>
        private Mutex _mutex = new System.Threading.Mutex(false, "Muzudho's WPF mutex practice");

        /// <summary>
        /// アプリケーションのエントリーポイントです
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!_mutex.WaitOne(0, false))
            {
                // TODO 既に起動しているウィンドウをアクティブ（最前面に表示）します。この機能は難しいので、今回は実装しません

                // 既に起動しているので終了させます
                _mutex.Close();
                _mutex = null;
                this.Shutdown();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (_mutex != null)
            {
                _mutex.ReleaseMutex();
                _mutex.Close();
            }
        }
    }
}
