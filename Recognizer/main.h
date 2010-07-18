#ifndef MAIN_H

#include <windows.h>
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include "recognizer.h"  // 중간 단계를 저장하는 파일이름들이 Define 되어 있는 헤더파일.



BOOL CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
void socket_init(void);


BOOL Open();



void init_controls(HWND hCombo ,HWND hCombo2, HWND hListCrl, HWND hListCrl_filter);




HWND hWnd;                    //메인 다이얼로그의 전역번수화, WinProc에서 시작할대 hwnd를 hWnd에 넣는다.
HWND hCombo;                  //boudrate를 설정하는 콤보박스
HWND hCombo2;                 //Comport를 설정하는 콤보박스
HWND hListCrl;                //Raw_data를 보여주는 콤보박스
HWND hListCrl_filter;         //Filted_data를 보여주는 콤보박스
char Items[][10]={"9600","19200","57600","115200"};   //2개의 콤보박스에 각자 넣는 문자 열들.
char Ports[][6]={"COM1","COM2","COM3","COM4","COM5","COM6","COM7","COM8","COM9","COM10","COM11","COM12","COM13","COM14","COM15","COM16","COM17","COM18","COM19"};
int Sending_flag = 0;         //데이터를 받는 부분이 쓰레드로 되어 있기때문에.
                              //그 쓰레드를 멈추게 할 Flag가 필요하다. 이 변수가 0이되면
                              //쓰레드가 종료하고 전송을 멈추게 된다.
int coordinate_cnt = 0;       //원래 모든 좌표들을 손실없이 처리 할꺼라고 생각하고 만든 변수. 
                              //실제로는 필터처리될때 일부 손실이 생기기때문에 현재는 의미 없다.
extern HANDLE g_handle;       //스레드를 대기하기 위한 전역 핸들.
                              //실제로는 Sending_flag를 쓰기때문에 지워야 할 부분.

extern int under_weak;
extern int weak_strong;
extern int strong_hard;
extern int over_hard;


#endif