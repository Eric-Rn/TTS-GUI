using Microsoft.Win32;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;

namespace TTS_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> installedVoices = new List<string>();
        SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
        public MainWindow()
        {
            InitializeComponent();
            foreach (InstalledVoice voice in speechSynthesizer.GetInstalledVoices())
            {
                VoiceInfo vinfo = voice.VoiceInfo;
                installedVoices.Add(vinfo.Name);
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = vinfo.Name + " | " + vinfo.Age + " | " + vinfo.Gender + " | " + vinfo.Culture;
                voice_sel.Items.Add(comboBoxItem);
            }
        }

        private void speakaloud_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                speechSynthesizer.SetOutputToDefaultAudioDevice();
                speechSynthesizer.SelectVoice(installedVoices[voice_sel.SelectedIndex]);
                speechSynthesizer.Speak(speak_content.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void speaktofile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = "Output";
                saveFileDialog.DefaultExt = ".wav";
                Nullable<bool> sfdresult = saveFileDialog.ShowDialog();
                if (sfdresult.Value)
                {
                    speechSynthesizer.SetOutputToWaveFile(saveFileDialog.FileName);
                    speechSynthesizer.SelectVoice(installedVoices[voice_sel.SelectedIndex]);
                    speechSynthesizer.Speak(speak_content.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}