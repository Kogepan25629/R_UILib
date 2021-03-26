using System;
using System.Collections.Generic;
using DxLibDLL;

// Version 0.6.1

namespace R_UILib
{
    static class RUI_Data
    {
        internal static int GrHandleBlack = DX.MakeGraph(1, 1);
        internal static int GrHandleWhite = DX.MakeGraph(1, 1);
        internal static void _RUI_DataInit()
        {
            DX.FillGraph(GrHandleBlack, 0, 0, 0, 255);
            DX.FillGraph(GrHandleWhite, 255, 255, 255, 255);
        }
    }
    internal class RUI_MouseData
    {
        //マウス座標
        internal static int MousePointX { get; private set; }
        internal static int MousePointY { get; private set; }
        //左クリックした瞬間のマウス座標
        internal static int MouseClickDownLeftPointX { get; private set; }
        internal static int MouseClickDownLeftPointY { get; private set; }
        //左クリックを離した瞬間のマウス座標
        internal static int MouseClickUpLeftPointX { get; private set; }
        internal static int MouseClickUpLeftPointY { get; private set; }
        //右クリックした瞬間のマウス座標
        internal static int MouseClickDownRightPointX { get; private set; }
        internal static int MouseClickDownRightPointY { get; private set; }
        //右クリックを離した瞬間のマウス座標
        internal static int MouseClickUpRightPointX { get; private set; }
        internal static int MouseClickUpRightPointY { get; private set; }
        //クリックしている間
        internal static bool MouseClickLeft { get; private set; }
        internal static bool MouseClickRight { get; private set; }
        //Oldクリックしている間
        internal static bool OldMouseClickLeft { get; private set; }
        internal static bool OldMouseClickRight { get; private set; }
        //クリックした瞬間
        internal static bool MouseClickDownLeft { get; private set; }
        internal static bool MouseClickDownRight { get; private set; }
        //クリックを離した瞬間
        internal static bool MouseClickUpLeft { get; private set; }
        internal static bool MouseClickUpRight { get; private set; }

        internal static void UptadeMouseState()
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
    }

    // Buttonの基底クラス
    public abstract class RUI_ButtonBase
    {
        public int X1;
        public int X2;
        public int Y1;
        public int Y2;
        public bool DetectionResult;
        private int LeftRight;
        private int UpDown;

        // コンストラクタ
        public RUI_ButtonBase()
        {
            X1 = -1;
            X2 = -1;
            Y1 = -1;
            Y2 = -1;
            DetectionResult = false;
            LeftRight = -1;
            UpDown = -1;
        }

        // 座標の設定
        public void SetPoint(int x1, int y1)
        {
            X1 = x1;
            Y1 = y1;
        }
        public void SetPoint(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public bool DetectOnCursor()
        {
            if (X1 == -1 || X2 == -1 || Y1 == -1 || Y2 == -1) {
                return false;
            }

            if (RUI_MouseData.MousePointX >= X1 && RUI_MouseData.MousePointX <= X2 && RUI_MouseData.MousePointY >= Y1 && RUI_MouseData.MousePointY <= Y2) {
                return true;
            }
            else {
                return false;
            }
        }

        //// 描画処理
        internal abstract int _Show(bool detectOnCursor);

        //// マウスクリックの判定
        internal bool _DetectMouseClick()
        {
            switch (LeftRight) {
                case RUI.RIGHT:
                    switch (UpDown) {
                        case RUI.HOLD: return MouseClickRightDetection();
                        case RUI.UP:   return MouseClickUpRightDetection();
                        case RUI.DOWN: return MouseClickDownRightDetection();
                        default: return false;
                    }
                case RUI.LEFT:
                    switch (UpDown) {
                        case RUI.HOLD: return MouseClickLeftDetection();
                        case RUI.UP:   return MouseClickUpLeftDetection();
                        case RUI.DOWN: return MouseClickDownLeftDetection();
                        default: return false;
                    }
                default:
                    return false;
            }
        }

        protected int _SetDetectionMethod(int leftRight, int upDown)
        {
            if (leftRight == RUI.RIGHT || leftRight == RUI.LEFT) {
                LeftRight = leftRight;
            }
            else {
                return -1;
            }
            if (upDown == RUI.HOLD || upDown == RUI.UP || upDown == RUI.DOWN) {
                UpDown = upDown;
            }
            else {
                return -1;
            }
            return 0;
        }

        //左クリックの判定
        protected bool MouseClickLeftDetection()
        {
            if (RUI_MouseData.MouseClickLeft == true && RUI_MouseData.MousePointX >= X1 && RUI_MouseData.MousePointX <= X2 && RUI_MouseData.MousePointY >= Y1 && RUI_MouseData.MousePointY <= Y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //左クリックの判定
        protected bool MouseClickRightDetection()
        {
            if (RUI_MouseData.MouseClickRight == true && RUI_MouseData.MousePointX >= X1 && RUI_MouseData.MousePointX <= X2 && RUI_MouseData.MousePointY >= Y1 && RUI_MouseData.MousePointY <= Y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //左クリックアップの判定
        protected bool MouseClickUpLeftDetection()
        {
            if (RUI_MouseData.MouseClickUpLeft == true && RUI_MouseData.MouseClickUpLeftPointX >= X1 && RUI_MouseData.MouseClickUpLeftPointX <= X2 && RUI_MouseData.MouseClickUpLeftPointY >= Y1 && RUI_MouseData.MouseClickUpLeftPointY <= Y2 && RUI_MouseData.MouseClickDownLeftPointX >= X1 && RUI_MouseData.MouseClickDownLeftPointX <= X2 && RUI_MouseData.MouseClickDownLeftPointY >= Y1 && RUI_MouseData.MouseClickDownLeftPointY <= Y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //右クリックアップの判定
        protected bool MouseClickUpRightDetection()
        {
            if (RUI_MouseData.MouseClickUpRight == true && RUI_MouseData.MouseClickUpRightPointX >= X1 && RUI_MouseData.MouseClickUpRightPointX <= X2 && RUI_MouseData.MouseClickUpRightPointY >= Y1 && RUI_MouseData.MouseClickUpRightPointY <= Y2 && RUI_MouseData.MouseClickDownRightPointX >= X1 && RUI_MouseData.MouseClickDownRightPointX <= X2 && RUI_MouseData.MouseClickDownRightPointY >= Y1 && RUI_MouseData.MouseClickDownRightPointY <= Y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //左クリックダウンの判定
        protected bool MouseClickDownLeftDetection()
        {
            if (RUI_MouseData.MouseClickDownLeft == true && RUI_MouseData.MouseClickDownLeftPointX >= X1 && RUI_MouseData.MouseClickDownLeftPointX <= X2 && RUI_MouseData.MouseClickDownLeftPointY >= Y1 && RUI_MouseData.MouseClickDownLeftPointY <= Y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //右クリックダウンの判定
        protected bool MouseClickDownRightDetection()
        {
            if (RUI_MouseData.MouseClickDownRight == true && RUI_MouseData.MouseClickDownRightPointX >= X1 && RUI_MouseData.MouseClickDownRightPointX <= X2 && RUI_MouseData.MouseClickDownRightPointY >= Y1 && RUI_MouseData.MouseClickDownRightPointY <= Y2) {
                return true;
            }
            else {
                return false;
            }
        }
    }

    //
    public class RUI_Manager
    {
        private List<RUI_ButtonManager> InsList = new List<RUI_ButtonManager>();  // インスタンスを格納するList
        private List<int> HandleList = new List<int>();                           // インスタンスのHandleを格納するList

        public int Add(RUI_ButtonManager rUI_ButtonManager)
        {
            return AddFirst(rUI_ButtonManager);
        }

        public void Detect(bool reset = false)
        {
            foreach (RUI_ButtonManager rUI_ButtonManager in InsList) {
                if (rUI_ButtonManager.Detect(reset) == true) {
                    break;
                }
            }
            // 各Listを初期化
            if (reset == true) {
                InsList = new List<RUI_ButtonManager>();
                HandleList = new List<int>();
            }
        }

        public void Show()
        {
            List<bool> onCursor = new List<bool>();
            bool detectOnCursor = true;

            for (int i = 0;i < InsList.Count; i++) {
                if (detectOnCursor == true) {
                    onCursor.Add(InsList[i].DetectOnCursor());
                    if (onCursor[i] == true) {
                        detectOnCursor = false;
                    }
                }
                else {
                    onCursor.Add(false);
                }
            }
            for (int i = InsList.Count-1; i >= 0; i--) {
                InsList[i].Show(onCursor[i]);
            }
        }

        public void Remove(int handle)
        {
            foreach (RUI_ButtonManager rUI_ButtonManager in InsList) {
                rUI_ButtonManager.ResetDetection();
            }
            int index = HandleList.IndexOf(handle);
            HandleList.RemoveAt(index);
            InsList.RemoveAt(index);
        }

        private int GenerateHandle()
        {
            int count = 0;
            bool loop = true;
            while (loop == true) {
                count++;
                loop = false;
                foreach (int lhandle in HandleList) {
                    if (lhandle == count) {
                        loop = true;
                    }
                }
            }
            return count;
        }

        private int AddFirst(RUI_ButtonManager rUI_ButtonManager)
        {
            // インスタンスをListの最初に挿入
            List<RUI_ButtonManager> tmpIns = new List<RUI_ButtonManager>(InsList);
            InsList.Clear();
            InsList.Add(rUI_ButtonManager);
            InsList.AddRange(tmpIns);

            // インスタンスに対応するHandleをListの最初に挿入
            List<int> tmpHandle = new List<int>(HandleList);
            //HandleList = new List<int>();
            int handle = GenerateHandle();
            HandleList.Clear();
            HandleList.Add(handle);
            HandleList.AddRange(tmpHandle);
            return handle;
        }
    }

    //
    public class RUI_ButtonManager
    {
        //
        private List<RUI_ButtonBase> InsList = new List<RUI_ButtonBase>();  // インスタンスを格納するList
        //private List<bool> DetList = new List<bool>();                      // 判定の結果を格納するList

        public void Add(RUI_ButtonBase rUI_ButtonBase)
        {
            InsList.Add(rUI_ButtonBase);
        }

        public bool Detect(bool reset = false)
        {
            bool funResult = false;
            foreach (RUI_ButtonBase rUI_ButtonBase in InsList) {
                rUI_ButtonBase.DetectionResult = rUI_ButtonBase._DetectMouseClick();
                if (rUI_ButtonBase.DetectionResult == true) {
                    funResult = true;
                }
            }

            // 各Listを初期化
            if (reset == true) {
                InsList = new List<RUI_ButtonBase>();
                //DetList = new List<bool>();
            }

            return funResult;
        }

        public bool DetectOnCursor()
        {
            foreach (RUI_ButtonBase rUI_ButtonBase in InsList) {
                if (rUI_ButtonBase.DetectOnCursor() == true) {
                    return true;
                }
            }
            return false;
        }
        
        public void ResetDetection()
        {
            foreach (RUI_ButtonBase rUI_ButtonBase in InsList) {
                rUI_ButtonBase.DetectionResult = false;
            }
        }
        
        public void Show(bool detectOnCursor)
        {
            foreach (RUI_ButtonBase rUI_ButtonBase in InsList) {
                rUI_ButtonBase._Show(detectOnCursor);
            }
        }
    }

    // RUIクラス
    public class RUI
    {
        //定数
        public const int RIGHT = 0;
        public const int LEFT = 1;
        public const int HOLD = 0;
        public const int UP = 1;
        public const int DOWN = 2;
        // マウス情報の更新
        // 毎フレーム呼び出しする必要がある
        public static new void UptadeMouseState()
        {
            RUI_MouseData.UptadeMouseState();
        }

        //RUI_Dataの初期化
        public static void R_UILibInit()
        {
            RUI_Data._RUI_DataInit();
        }
    }

    // RUI_Buttonクラス
    public class RUI_Button : RUI_ButtonBase
    {
        public int GrHandle;
        public int GrHandle2;
        public int FontHandle;
        public uint StrColor;
        private bool MatchSizeToString;
        private bool OnCursorHighlight;
        private bool OnCursorChangeImage;
        private string Str;
        private int StringWidth;
        private int StringHeight;

        ////コンストラクタ
        public RUI_Button()
        {
            GrHandle = -1;
            GrHandle2 = -1;
            FontHandle = -1;
            StrColor = DX.GetColor(255, 255, 255);
            MatchSizeToString = false;
            OnCursorHighlight = false;
            OnCursorChangeImage = false;
            Str = "";
            StringWidth = -1;
            StringHeight = -1;
        }

        //// ボタンモード変更
        public void SetAllMode(bool matchSizeToString, bool onCursorHighlight, bool onCursorChangeImage)
        {
            MatchSizeToString = matchSizeToString;
            OnCursorHighlight = onCursorHighlight;
            OnCursorChangeImage = onCursorChangeImage;
        }

        //// ボタンテキスト(Str)の変更
        // PRIVATEメゾッド
        private int _SetString(string str)
        {
            if (FontHandle != -1) {
                Str = str;
                StringWidth = DX.GetDrawStringWidthToHandle(Str, Str.Length, FontHandle);
                StringHeight = DX.GetFontSizeToHandle(FontHandle);
                return 0;
            }
            else {
                return -1;
            }
        }
        // 外部参照
        public int SetString(string str, int fontHandle)
        {
            FontHandle = fontHandle;
            return _SetString(str);
        }
        public int SetString(string str)
        {
            return _SetString(str);
        }

        // PRIVATE
        private int ChangeSizeToString()
        {
            if (MatchSizeToString == true) {
                if (Str == "" || StringWidth == -1 || StringHeight == -1) {
                    return -1;
                }
                X2 = X1 + StringWidth;
                Y2 = Y1 + StringHeight;
            }
            return 0;
        }

        //// ボタン描画
        internal override int _Show(bool onCursor)
        {
            // ボタンサイズを文字列に合わせる(モードによる)
            if (ChangeSizeToString() == -1) {
                return -1;
            }
            // 値が設定されていない場合は終了
            if (X1 == -1 || Y1 == -1 || X2 == -1 || Y2 == -1) {
                return -1;
            }

            // 画像の描画
            if (GrHandle != -1) {
                int drawGrHandle = GrHandle;

                if (OnCursorChangeImage == true) {
                    if (GrHandle2 != -1) {
                        if (DetectOnCursor() == true && onCursor == true) {
                            drawGrHandle = GrHandle2;
                        }
                    }
                }

                if (OnCursorHighlight == true) {
                    if (DetectOnCursor() == true && onCursor == true) {
                        DX.DrawExtendGraph(X1, Y1, X2, Y2, RUI_Data.GrHandleWhite, DX.TRUE);
                    }
                    else {
                        DX.DrawExtendGraph(X1, Y1, X2, Y2, RUI_Data.GrHandleBlack, DX.TRUE);
                    }
                    DX.DrawExtendGraph(X1 + 1, Y1 + 1, X2 - 1, Y2 - 1, drawGrHandle, DX.TRUE);
                }
                else {
                    DX.DrawExtendGraph(X1, Y1, X2, Y2, drawGrHandle, DX.TRUE);
                }
            }

            // 文字列の描画
            if (Str != "") {
                if (MatchSizeToString) {
                    DX.DrawString(X1, Y1, Str, StrColor);
                }
                else {
                    DX.DrawString(X1 + (((X2 - X1) - StringWidth) / 2), Y1 + (((Y2 - Y1) - StringHeight) / 2), Str, StrColor);
                }
            }

            return 0;
        }

        public int Show()
        {
            return _Show(true);
        }

        //// マウスクリックの判定
        public bool DetectMouseClick(int LeftRight, int UpDown)
        {
            // ボタンサイズを文字列に合わせる(モードにより)
            if (ChangeSizeToString() == -1) {
                return false;
            }
            _SetDetectionMethod(LeftRight, UpDown);
            return _DetectMouseClick();
        }

        // 判定の設定
        public int SetDetectionMethod(int LeftRight, int UpDown)
        {
            return _SetDetectionMethod(LeftRight, UpDown);
        }
    }
}