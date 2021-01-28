using DxLibDLL;
using System;

// Version 0.1

namespace R_UILib
{
    public abstract class RUI_MouseData
    {
        //マウス座標
        protected static int MousePointX { get; private set; }
        protected static int MousePointY { get; private set; }
        //左クリックした瞬間のマウス座標取得
        protected static int MouseClickDownLeftPointX { get; private set; }
        protected static int MouseClickDownLeftPointY { get; private set; }
        //左クリックを離した瞬間のマウス座標取得
        protected static int MouseClickUpLeftPointX { get; private set; }
        protected static int MouseClickUpLeftPointY { get; private set; }
        //右クリックした瞬間のマウス座標
        protected static int MouseClickDownRightPointX { get; private set; }
        protected static int MouseClickDownRightPointY { get; private set; }
        //右クリックを離した瞬間のマウス座標
        protected static int MouseClickUpRightPointX { get; private set; }
        protected static int MouseClickUpRightPointY { get; private set; }
        //クリックしている間
        protected static bool MouseClickLeft { get; private set; }
        protected static bool MouseClickRight { get; private set; }
        //Oldクリックしている間
        protected static bool OldMouseClickLeft { get; private set; }
        protected static bool OldMouseClickRight { get; private set; }
        //クリックした瞬間
        protected static bool MouseClickDownLeft { get; private set; }
        protected static bool MouseClickDownRight { get; private set; }
        //クリックを離した瞬間
        protected static bool MouseClickUpLeft { get; private set; }
        protected static bool MouseClickUpRight { get; private set; }

        protected static void UptadeMouseState()
        {
            OldMouseClickLeft = MouseClickLeft;
            OldMouseClickRight = MouseClickRight;
            //クリックしているかどうか判定
            if ((DX.GetMouseInput() & DX.MOUSE_INPUT_LEFT) != 0) {
                MouseClickLeft = true;
            }
            else {
                MouseClickLeft = false;
            }
            if ((DX.GetMouseInput() & DX.MOUSE_INPUT_RIGHT) != 0) {
                MouseClickRight = true;
            }
            else {
                MouseClickRight = false;
            }
            //クリックした瞬間を判定
            if (OldMouseClickLeft == false && MouseClickLeft == true) {
                MouseClickDownLeft = true;
            }
            else {
                MouseClickDownLeft = false;
            }
            if (OldMouseClickRight == false && MouseClickRight == true) {
                MouseClickDownRight = true;
            }
            else {
                MouseClickDownRight = false;
            }
            //クリックを離した瞬間を判定
            if (OldMouseClickLeft == true && MouseClickLeft == false) {
                MouseClickUpLeft = true;
            }
            else {
                MouseClickUpLeft = false;
            }
            if (OldMouseClickRight == true && MouseClickRight == false) {
                MouseClickUpRight = true;
            }
            else {
                MouseClickUpRight = false;
            }
            //マウス座標取得
            {
                {
                    DX.GetMousePoint(out int mousePointX, out int mousePointY);
                    MousePointX = mousePointX;
                    MousePointY = mousePointY;
                }
            }
            //左クリックした瞬間のマウス座標取得
            if (MouseClickDownLeft == true) {
                {
                    DX.GetMousePoint(out int mouseClickDownLeftPointX, out int mouseClickDownLeftPointY);
                    MouseClickDownLeftPointX = mouseClickDownLeftPointX;
                    MouseClickDownLeftPointY = mouseClickDownLeftPointY;
                }
            }
            //左クリックを離した瞬間のマウス座標取得
            if (MouseClickUpLeft == true) {
                {
                    DX.GetMousePoint(out int mouseClickUpLeftPointX, out int mouseClickUpLeftPointY);
                    MouseClickUpLeftPointX = mouseClickUpLeftPointX;
                    MouseClickUpLeftPointY = mouseClickUpLeftPointY;
                }
            }
            //右クリックした瞬間のマウス座標取得
            if (MouseClickDownRight == true) {
                {
                    DX.GetMousePoint(out int mouseClickDownRightPointX, out int mouseClickDownRightPointY);
                    MouseClickDownRightPointX = mouseClickDownRightPointX;
                    MouseClickDownRightPointY = mouseClickDownRightPointY;
                }
            }
            //右クリックを離した瞬間のマウス座標取得
            if (MouseClickUpRight == true) {
                {
                    DX.GetMousePoint(out int mouseClickUpRightPointX, out int mouseClickUpRightPointY);
                    MouseClickUpRightPointX = mouseClickUpRightPointX;
                    MouseClickUpRightPointY = mouseClickUpRightPointY;
                }
            }
        }

        //左クリックの判定
        protected static bool MouseClickLeftDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickLeft == true && MousePointX >= x1 && MousePointX <= x2 && MousePointY >= y1 && MousePointY <= y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //左クリックの判定
        protected static bool MouseClickRightDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickRight == true && MousePointX >= x1 && MousePointX <= x2 && MousePointY >= y1 && MousePointY <= y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //左クリックアップの判定
        protected static bool MouseClickUpLeftDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickUpLeft == true && MouseClickUpLeftPointX >= x1 && MouseClickUpLeftPointX <= x2 && MouseClickUpLeftPointY >= y1 && MouseClickUpLeftPointY <= y2 && MouseClickDownLeftPointX >= x1 && MouseClickDownLeftPointX <= x2 && MouseClickDownLeftPointY >= y1 && MouseClickDownLeftPointY <= y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //右クリックアップの判定
        protected static bool MouseClickUpRightDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickUpRight == true && MouseClickUpRightPointX >= x1 && MouseClickUpRightPointX <= x2 && MouseClickUpRightPointY >= y1 && MouseClickUpRightPointY <= y2 && MouseClickDownRightPointX >= x1 && MouseClickDownRightPointX <= x2 && MouseClickDownRightPointY >= y1 && MouseClickDownRightPointY <= y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //左クリックダウンの判定
        protected static bool MouseClickDownLeftDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickDownLeft == true && MouseClickDownLeftPointX >= x1 && MouseClickDownLeftPointX <= x2 && MouseClickDownLeftPointY >= y1 && MouseClickDownLeftPointY <= y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //右クリックダウンの判定
        protected static bool MouseClickDownRightDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickDownRight == true && MouseClickDownRightPointX >= x1 && MouseClickDownRightPointX <= x2 && MouseClickDownRightPointY >= y1 && MouseClickDownRightPointY <= y2) {
                return true;
            }
            else {
                return false;
            }
        }

    }

    // RUI_Buttonクラス
    class RUI_Button : RUI_MouseData
    {
        protected int Mode;
        protected int X1;
        protected int X2;
        protected int Y1;
        protected int Y2;
        protected int GrHandle;
        protected int FontHandle;
        protected uint StrColor;
        private   string Str;
        //コンストラクタ
        public RUI_Button()
        {
            Mode = 0;
            X1 = -1;
            X2 = -1;
            Y1 = -1;
            Y2 = -1;
            GrHandle = -1;
            FontHandle = -1;
            StrColor = DX.GetColor(255, 255, 255);
            Str = "";
        }

        // ボタンテキスト(Str)の変更
        public int SetString(string str)
        {
            if (Mode == 1) {
                if (FontHandle != -1 && X1 != -1 && Y1 != -1 && X2 != -1 && Y2 != -1) {
                    X2 = X1 + DX.GetDrawStringWidthToHandle(str, str.Length, FontHandle);
                    Y2 = Y1 + DX.GetFontSizeToHandle(FontHandle);

                    return 0;
                }
                else {
                    return -1;
                }
            }
            else {
                Str = str;
                return 0;
            }
        }


        //ボタン描画
        protected void Show()
        {
            if (GrHandle != -1) {

            }
        }

        //左クリックの判定
        protected bool LeftDetection()
        {
            return MouseClickLeftDetection(X1, Y1, X2, Y2);
        }
        //右クリックの判定
        protected bool RightDetection()
        {
            return MouseClickLeftDetection(X1, Y1, X2, Y2);
        }
        //左クリックアップの判定
        protected bool LeftUpDetection()
        {
            return MouseClickUpLeftDetection(X1, Y1, X2, Y2);
        }
        //右クリックアップの判定
        protected bool RightUpDetection()
        {
            return MouseClickUpRightDetection(X1, Y1, X2, Y2);
        }
        //左クリックダウンの判定
        protected bool LeftDownDetection()
        {
            return MouseClickDownLeftDetection(X1, Y1, X2, Y2);
        }
        //右クリックダウンの判定
        protected bool RightDownDetection()
        {
            return MouseClickDownRightDetection(X1, Y1, X2, Y2);
        }
    }
    // RUIクラス
    abstract class RUI : RUI_MouseData
    {
        // マウス情報の更新
        // 毎フレーム呼び出しする必要がある
        public static new void UptadeMouseState()
        {
            RUI_MouseData.UptadeMouseState();
        }
    }


    class RUI_ButtonImage : RUI_MouseData
    {
        // 画像のみ
        public bool Show(int x1, int y1, int x2, int y2, int grhandle)
        {
            DX.DrawExtendGraph(x1, y2, x2, y2, grhandle, DX.TRUE);
            if (MouseClickUpLeftDetection(x1, y1, x2, y2) == true) {
                return true;
            }
            else {
                return false;
            }
        }
    }
    class RUI_ButtonString : RUI_MouseData
    {
        // 文字列のみ
        // 文字中央揃え
        // ボタンサイズ自由
        public bool Show(int x1, int y1, int x2, int y2, string str, int fonthandle, uint color)
        {
            DX.DrawString(x1 + (((x2 - x1) - DX.GetDrawStringWidthToHandle(str, str.Length, fonthandle)) / 2), y1 + (((y2 - y1) - DX.GetFontSizeToHandle(fonthandle)) / 2), str, color);
            if (MouseClickUpLeftDetection(x1, y1, x2, y2) == true) {
                return true;
            }
            else {
                return false;
            }
        }
        // 文字列のみ
        public bool Show(int x1, int y1, string str, int fonthandle, uint color)
        {
            int x2 = x1 + DX.GetDrawStringWidthToHandle(str, str.Length, fonthandle);
            int y2 = y1 + DX.GetFontSizeToHandle(fonthandle);
            DX.DrawString(x1, y1, str, color);

            if (MouseClickUpLeftDetection(x1, y1, x2, y2) == true) {
                return true;
            }
            else {
                return false;
            }
        }
    }

    class RUI_ButtonImageString : RUI_MouseData
    {
        // 画像と文字列
        // 文字中央揃え
        // ボタンサイズ自由
        public bool Show(int x1, int y1, int x2, int y2, string str, int fonthandle, uint color, int grhandle)
        {
            DX.DrawExtendGraph(x1, y1, x2, y2, grhandle, DX.TRUE);
            DX.DrawString(x1 + (((x2 - x1) - DX.GetDrawStringWidthToHandle(str, str.Length, fonthandle)) / 2), y1 + (((y2 - y1) - DX.GetFontSizeToHandle(fonthandle)) / 2), str, color);
            if (MouseClickUpLeftDetection(x1, y1, x2, y2) == true) {
                return true;
            }
            else {
                return false;
            }
        }
        // 画像と文字列
        public bool Show(int x1, int y1, string str, int fonthandle, uint color, int grhandle)
        {
            int x2 = x1 + DX.GetDrawStringWidthToHandle(str, str.Length, fonthandle);
            int y2 = y1 + DX.GetFontSizeToHandle(fonthandle);
            DX.DrawExtendGraph(x1, y1, x2, y2, grhandle, DX.TRUE);
            DX.DrawString(x1, y1, str, color);
            if (MouseClickUpLeftDetection(x1, y1, x2, y2) == true) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}

