using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RSSFeedLibrary
{
    public class FeedParser : IDisposable
    {  
        /// <summary>
        /// 비동기 파싱 시작 
        /// </summary>
        /// <param name="reciever">공급 대상자</param>
        /// <returns>파싱성공 여부</returns>
        public bool BeginParse(IFeedProvider reciever)
        {            
            //델리게이트 연결 

            //XML이 RSS내지 podcast 인지 구별 

            //받은 XML을 쭉 읽어서 각 태그별로 뽑아다가 피드에 삽입 

            //각 피드에서 특정 정보를 얻어내는 것은 IFeedReciever의 구현에 따라 결정 

            return false;
        }

        /// <summary>
        /// 비동기 파싱 중지
        /// 다운로드가 오래 걸리거나 Hang 될때 중단
        /// </summary>
        public void EndParse(IFeedProvider reciever)
        {
            //해당 파서에 대한 피딩 중단
            //프로바이더 제거
        }

        public void Dispose()
        {
            
        }
    }
}
