using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot.Args;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram;
using System.Runtime;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO;
using System.Net;

namespace TelegramAPI_TEST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        TelegramBotClient client = null;
        List<Update> updates = new List<Update>();
       // List<long> ids = null;
        System.Threading.Timer timer = null;


        private  void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                client = new TelegramBotClient("6368646450:AAGBCksctBOXkG26Ha-of_o3D6EE7oue__E");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            client.StartReceiving(Update, Error);
            timer = new System.Threading.Timer(InternetCheck, null, 0, 100);

           
        }


        public async void WhoWriteNowInMyFuckinBot(Update update)
        {
            await Task.Delay(100);
            string WhoWriteNow = "@" + update.Message.Chat.Username + " Имя: " + update.Message.Chat.FirstName;
            string WhatWriteNow = update.Message.Text;
           

            this.Invoke(new Action(() => 
            {
                label1.Text = WhoWriteNow;
                label2.Text = WhatWriteNow;
            }));

        }

       

        private async Task Update(ITelegramBotClient cli, Update update, CancellationToken token)
        {

            bot = (TelegramBotClient)cli;
            up = update;
                        
            var message = update.Message;

            WhoWriteNowInMyFuckinBot(update);
                        
            if (message.Text != null)
            {


                if (message.Text.ToLower().Contains("привет"))
                {
                    await cli.SendTextMessageAsync(message.Chat.Id, "Привет, ты как. Спал???");

                }
                else if (message.Text.ToLower().Contains("число"))
                {
                    await cli.SendTextMessageAsync(message.Chat.Id, $"{new Random().Next()}");
                    
                }
                
            }


        }

        private async Task Error(ITelegramBotClient cli, Exception exception, CancellationToken token)
        {


           
            
        }


        TelegramBotClient bot = null;
        Update up = null;

        private void button1_Click(object sender, EventArgs e)
        {

            
            







            //var upd = await bot.GetUpdatesAsync();

            //var lasmes = upd[upd.Length - 1].Message;

            //var photoSize = lasmes.Photo.OrderByDescending(p => p.FileSize).FirstOrDefault();

            //var file = await bot.GetFileAsync(photoSize.FileId);

            //MemoryStream stream = new MemoryStream();
            //await bot.DownloadFileAsync(file.FilePath, stream);

            //var bitmap = new Bitmap(stream);
            //pictureBox1.Image = bitmap;


        }

        


        private async void InternetCheck(Object o)
        {

           
            if (IsNetworkAvailable().Result == false)    
            {    
                timer.Change(Timeout.Infinite, Timeout.Infinite);

                this.Invoke(new Action(() => { label3.Text = "Подключение к Интернету";}));
                    
                var dotTimer = new System.Threading.Timer(Dots, null, 0, 600);    

                await Task.Run(() =>    
                {    

                    while (true)    
                    {    


                        Thread.Sleep(100);    
                        if (IsNetworkAvailable().Result == true)    
                        {    
                            client = new TelegramBotClient("6368646450:AAGBCksctBOXkG26Ha-of_o3D6EE7oue__E");    
                            client.StartReceiving(Update, Error);    
                            timer.Change(0, 100);    
                            dotTimer.Change(Timeout.Infinite, Timeout.Infinite);
                            dotTimer = null;    
                            this.Invoke(new Action(() => label3.Text = null));    
                            break;    
                        }    

                    }    
                });    
            }    
                   

        }
        
        private void Dots(Object o)
        {
            try
            {
                if (IsNetworkAvailable().Result == false)
                {
                    this.Invoke(new Action(() =>
                    {
                        label3.Text += ".";


                        if (label3.Text == "Подключение к Интернету....")
                        {
                            label3.Text = "Подключение к Интернету";
                        }


                    }));
                }

                else if (IsNetworkAvailable().Result == true)
                {
                    this.Invoke(new Action(() => { label3.Text = null; }));
                    
                }
            }
            catch (Exception) { }
        }

        private async Task<bool> IsNetworkAvailable()
            {
            return await Task<bool>.Run(()=>
            {
                
                Ping ping = new Ping();
                try
                {
                    PingReply reply = ping.Send("www.google.com");
                    return reply.Status == IPStatus.Success;
                }
                catch (PingException)
                {
                    return false;
                                       
                }
               
            });
        }

    }


}

