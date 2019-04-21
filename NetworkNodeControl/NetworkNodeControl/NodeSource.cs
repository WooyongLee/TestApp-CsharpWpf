using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkNodeControl
{
    #region 노드 그리기 관련 GCS_Main 구조체
    ////TWDLN_DL
    //struct TRA_DL
    //{
    //    uint DL_TermID;     // 송신처리 단말 식별자
    //    //uint RB_Assign;     // Bitmap 정보
    //    //uint MCSLevel;      //			(1~8)
    //    //uint ReqTxPower;        // [dBm]	(0~40)
    //    //uint AdvTime;       // [ms]		(0~0.25, resolution:24Mhz)
    //    //uint VedioAssign;   //			(0:TM, 1:TM/영상)

    //}

    //// TWDLN_RA_FrameUnit
    //struct TRA_FrameUnit
    //{
    //    uint UnitNo;
    //    uint UL_GrantTermID;
    //    //uint RATL_ULTxAnt;
    //    uint DLGrantSize; //  DL_Grant.Length
    //    List <TRA_DL> DL_Grant;

    //}

    //// TWDLN_ResourceAlloc
    //struct TWDLN_RA
    //{
    //    uint RACH_Index;
    //    uint CurrentFrmUnitNo;
    //    uint FrameUnitSize; // FrameUnit.Length
    //    List<TRA_FrameUnit> FrameUnit;

    //}
    #endregion

    public class RAFrameUnit
    {
        public int UL_GrantTermID;
        public List<int> ConnectedTermIDList; // 연결된 노드의 리스트
    }
}
