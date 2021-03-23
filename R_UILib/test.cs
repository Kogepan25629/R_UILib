using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using R_UILib;
//
namespace R_RPG
{
    class test
    {
        //ウィンドウサイズを格納する変数
        static public int Window_Width, Window_Heigt;
        static public void class2()
        {
            //Dxlib　前処理
            {
                //垂直同期
                DX.SetWaitVSyncFlag(DX.TRUE);

                {//ウィンドウモード

                    DX.SetWindowStyleMode(9);

                    Window_Width = 854; Window_Heigt = 480;
                    DX.SetGraphMode(Window_Width, Window_Heigt, 32);
                    DX.ChangeWindowMode(DX.TRUE);

                    // window のサイズ変更の可不可
                    DX.SetWindowSizeChangeEnableFlag(DX.FALSE);
                }
            }

            //DxLib初期化
            if (DX.DxLib_Init() == -1)
            {
                return;
            }
            //描写を裏画面に指定
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);

            DX.SetMouseDispFlag(DX.TRUE);

            int FontHandle = DX.CreateFontToHandle(null, -1, -1);

            int grhandle = DX.LoadGraph("Texture\\diamond_block.png");

            RUI.R_UILibInit();
            /*
            RUI_Button Button1 = new RUI_Button(false, true);
            Button1.X1 = 3;
            Button1.SetPoint(0, 0, 100, 100);
            Button1.GrHandle = grhandle;
            Button1.FontHandle = FontHandle;
            Button1.SetString("おはよう");
            */
            RUI_Button[] buttons = new RUI_Button[10];
            RUI_ButtonManager[] buttonManagers = new RUI_ButtonManager[10];
            RUI_Manager rUI_Manager = new RUI_Manager();
            int[] handle = new int[10];
            {
                int i = 0;
                foreach (RUI_Button rUI_Button in buttons) {
                    buttons[i] = new RUI_Button(false, true);
                    i++;
                }
                i = 0;
                foreach (RUI_Button rUI_Button in buttons) {
                    rUI_Button.X1 = 3;
                    rUI_Button.SetPoint(0+i*5, 0+i*5, 100+i*5, 100+i*5);
                    rUI_Button.GrHandle = grhandle;
                    rUI_Button.FontHandle = FontHandle;
                    rUI_Button.SetString("おはよう");
                    rUI_Button.SetDetectionMethod(RUI.LEFT, RUI.UP);
                    i++;
                }
            }
            {
                int i = 0;
                foreach (RUI_ButtonManager rUI_ButtonManager in buttonManagers) {
                    buttonManagers[i] = new RUI_ButtonManager();
                    i++;
                }
                i = 0;
                foreach (RUI_ButtonManager rUI_ButtonManager in buttonManagers) {
                    rUI_ButtonManager.Add(buttons[i]);
                    handle[i] = rUI_Manager.Add(rUI_ButtonManager);
                    //Console.WriteLine(handle[i]);
                    i++;
                }
            }

            

            while (DX.ScreenFlip() == 0 && DX.ProcessMessage() == 0 && DX.ClearDrawScreen() == 0)
            {
                RUI.UptadeMouseState();
                foreach (RUI_Button rUI_Button in buttons) {
                }
                rUI_Manager.Detect();
                rUI_Manager.Show();
                {
                    int i = 0;
                    foreach (RUI_Button rUI_Button in buttons) {
                        if (rUI_Button.DetectionResult == true) {
                            rUI_Manager.Remove(handle[i]);
                            Console.WriteLine("Detected!!!!!!!!!!!!!!!!!!");
                        }
                        i++;
                    }
                }
                /*
                Button1.Show();
                if (Button1.DetectMouseClick(RUI.LEFT, RUI.UP) == true)
                {
                    Console.WriteLine("Clicked");
                }
                */
            }
        }
    }
}
