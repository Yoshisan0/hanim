using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public enum EnmMarkElement
    {
        NONE,
        UP,
        IN
    }

    public enum EnmTypeParam
    {
        NONE,
        DISPLAY,
        POSITION_X,
        POSITION_Y,
        ROTATION_Z,
        SCALE_X,
        SCALE_Y,
        OFFSET_X,
        OFFSET_Y,
        FLIP_HORIZONAL,
        FLIP_VERTICAL,
        TRANSPARENCY,
        COLOR,
        USER_DATA,
    }

    public enum EnmTypeOption
    {
        NONE,
        DISPLAY,        //TYPE_PARAM.DISPLAY
        POSITION,       //TYPE_PARAM.POSITION_X TYPE_PARAM.POSITION_Y
        ROTATION,       //TYPE_PARAM.ROTATION_Z
        SCALE,          //TYPE_PARAM.SCALE_X TYPE_PARAM.SCALE_Y
        OFFSET,         //TYPE_PARAM.OFFSET_X TYPE_PARAM.OFFSET_Y
        FLIP,           //TYPE_PARAM.FLIP_HORIZONAL TYPE_PARAM.FLIP_VERTICAL
        TRANSPARENCY,   //TYPE_PARAM.TRANSPARENCY
        COLOR,          //TYPE_PARAM.COLOR
        USER_DATA,      //TYPE_PARAM.USER_DATA
    }

    public enum EnmParam
    {
        NONE = 0,

        POSITION_X,
        POSITION_Y,
        ROTATION,
        SCALE_X,
        SCALE_Y,
        TRANS,
        FLIP_H,
        FLIP_V,
        VISIBLE,
        COLOR,
        OFFSET_X,
        OFFSET_Y,
        USERDATA,
    }
}
