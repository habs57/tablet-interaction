#ifndef MAIN_H

#include <windows.h>
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include "recognizer.h"  // �߰� �ܰ踦 �����ϴ� �����̸����� Define �Ǿ� �ִ� �������.



BOOL CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
void socket_init(void);


BOOL Open();



void init_controls(HWND hCombo ,HWND hCombo2, HWND hListCrl, HWND hListCrl_filter);




HWND hWnd;                    //���� ���̾�α��� ��������ȭ, WinProc���� �����Ҵ� hwnd�� hWnd�� �ִ´�.
HWND hCombo;                  //boudrate�� �����ϴ� �޺��ڽ�
HWND hCombo2;                 //Comport�� �����ϴ� �޺��ڽ�
HWND hListCrl;                //Raw_data�� �����ִ� �޺��ڽ�
HWND hListCrl_filter;         //Filted_data�� �����ִ� �޺��ڽ�
char Items[][10]={"9600","19200","57600","115200"};   //2���� �޺��ڽ��� ���� �ִ� ���� ����.
char Ports[][6]={"COM1","COM2","COM3","COM4","COM5","COM6","COM7","COM8","COM9","COM10","COM11","COM12","COM13","COM14","COM15","COM16","COM17","COM18","COM19"};
int Sending_flag = 0;         //�����͸� �޴� �κ��� ������� �Ǿ� �ֱ⶧����.
                              //�� �����带 ���߰� �� Flag�� �ʿ��ϴ�. �� ������ 0�̵Ǹ�
                              //�����尡 �����ϰ� ������ ���߰� �ȴ�.
int coordinate_cnt = 0;       //���� ��� ��ǥ���� �սǾ��� ó�� �Ҳ���� �����ϰ� ���� ����. 
                              //�����δ� ����ó���ɶ� �Ϻ� �ս��� ����⶧���� ����� �ǹ� ����.
extern HANDLE g_handle;       //�����带 ����ϱ� ���� ���� �ڵ�.
                              //�����δ� Sending_flag�� ���⶧���� ������ �� �κ�.

extern int under_weak;
extern int weak_strong;
extern int strong_hard;
extern int over_hard;


#endif