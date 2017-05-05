using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public enum ELEMENTS_TYPE
    {
        Image,
        Shape,
        Joint,
        Effect,
        Accessory,
        FX
    }

    public enum ELEMENTS_STYLE
    {
        Rect,
        Circle,
        Point
    }

    public enum ELEMENTS_MARK
    {
        NONE,
        UP,
        IN
    }

    public enum TYPE_OPTION
    {
        NONE,
        DISPLAY,
        POSITION_X,
        POSITION_Y,
        ROTATION,
        SCALE_X,
        SCALE_Y,
        TRANSPARENCY,
        FLIP_HORIZONAL,
        FLIP_VERTICAL,
        COLOR,
        OFFSET_X,
        OFFSET_Y,
        USER_DATA,
    }
}
