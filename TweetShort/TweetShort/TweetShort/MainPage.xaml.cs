using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Net.NetworkInformation;
using Microsoft.Phone.Tasks;

namespace TweetShort
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            bool netWork = NetworkInterface.GetIsNetworkAvailable();
            if (!netWork)
            {
                textBlock1.Visibility = Visibility.Collapsed;
                textBlock2.Visibility = Visibility.Collapsed;
                textBox1.Text = "No Internet Connection Found !! Please Connect To Use This App";
                button1.Visibility = Visibility.Collapsed;
                ApplicationBar.IsVisible = false;

            }

        }

        public class RootObject
        {
            public int difference { get; set; }
            public string text { get; set; }
            public string original_text { get; set; }

        }

        private void GetData()
        {
            this.Dispatcher.BeginInvoke(delegate()
            {
               


                //this.busyIndicator.IsEnabled = true;

                string uri = String.Format("http://tweetshrink.com/shrink?text={0}", textBox1.Text);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(uri));
                //request.Credentials(" "," ");
                request.BeginGetResponse(new AsyncCallback(ReadCallback), request);
                progressBar1.Visibility = System.Windows.Visibility.Visible;


            });
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter the text and try again!", "TweetShort", MessageBoxButton.OK);
                textBox1.Text = "";
                //textBox1.Focus();
            }
            else
            {
                GetData();
            }
        }
    
        private void ReadCallback(IAsyncResult asynchronousResult)
        {
            

                Deployment.Current.Dispatcher.BeginInvoke(delegate() //fix it ...kkkkk thanx !!! lis
            {
                //this.busyIndicator.IsRunning = true;ten listen use this account for unlocking the phone propressinstruments@hotmail.com pwd rb1802b57 so what will this do? ummm....how can u register your phne ? ek bar phone ko dev locked kar ke phir se is account se unlock akr !! this is a developer account, who does it belong to? me. Ok so why should I use it?as i really dont think that you would use your Promo code !!and make 1 more account.. Haha, are u serious ? I i gdy redeemed my code :P great!!then unlock kar ek baar aur and dekh le 10 apps ho jati h for developers.hm Ill see so how is the code? which code?the present one?
                try
                {
                    HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
                    HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);

                    using (StreamReader streamReader1 = new StreamReader(response.GetResponseStream()))
                    {
                        string resultString = streamReader1.ReadToEnd();
                        var ser = new DataContractJsonSerializer(typeof(RootObject));

                        var stream = new MemoryStream(Encoding.Unicode.GetBytes(resultString));
                        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(RootObject));
                        RootObject myTweet = (RootObject)jsonSerializer.ReadObject(stream);
                        //  System.Threading.Thread.Sleep(5000);
                        // this.busyIndicator.IsEnabled = false;




                        textBox2.Text =myTweet.text.ToString();
                        progressBar1.Visibility = System.Windows.Visibility.Collapsed;
                        textBlock2.Visibility = System.Windows.Visibility.Visible;
                        textBox2.Visibility = System.Windows.Visibility.Visible;


                    }
                }
                catch
                {
                   // this.busyIndicator.IsRunning = false;
                   // RadMessageBox.Show("G URL Extract", MessageBoxButtons.OK,"URL Doesnt Exist ");
                   // textBlock3.Text = string.Empty;
                    

                }
    
        });

        }

        private void twit_click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Please enter some text and try again!", "TweetShort", MessageBoxButton.OK);
                textBox1.Text = "";
                //textBox1.Focus();
            }
            else
            {
                ShareStatusTask sharestat = new ShareStatusTask();
                sharestat.Status = textBox2.Text;
                sharestat.Show();
            }

        }
    }
}
