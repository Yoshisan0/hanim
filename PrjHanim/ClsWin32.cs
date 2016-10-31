using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PrjHikariwoAnim
{
    public static class ClsWin32
    {
        [DllImport("comctl32.dll")]
        public static extern bool ImageList_BeginDrag(
            IntPtr himlTrack,  // イメージリストのハンドル
            int iTrack,        // ドラッグするイメージの番号
            int dxHotspot,     // ドラッグ位置 (イメージ位置との相対座標)
            int dyHotspot      //
            );

        [DllImport("comctl32.dll")]
        public static extern bool ImageList_DragEnter(
            IntPtr hwndLock,   // ドラッグするイメージの親となるウィンドウのハンドル
            int x,    // ドラッグするイメージの表示位置 (ウィンドウ位置との相対座標)
            int y     //
            );

        [DllImport("comctl32.dll")]
        public static extern bool ImageList_DragLeave(
            IntPtr hwndLock    // ドラッグするイメージの親となるウィンドウのハンドル
            );

        [DllImport("comctl32.dll")]
        public static extern bool ImageList_DragMove(
            int x,    // ドラッグするイメージの表示位置 (ウィンドウ位置との相対座標)
            int y     //
            );

        [DllImport("comctl32.dll")]
        public static extern void ImageList_EndDrag();


        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
    }
}
