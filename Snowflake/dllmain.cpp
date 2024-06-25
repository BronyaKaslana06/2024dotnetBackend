// dllmain.cpp : 定义 DLL 应用程序的入口点。
#include "pch.h"
#include "SnowflakeIDcreator.h"
#include "EasyIDCreator.h"


extern "C" {
    __declspec(dllexport) long long NextId() {
        return SnowflakeIDcreator::nextId();
    }

    __declspec(dllexport) void SetWorkerId(int id) {
        SnowflakeIDcreator::SetWorkerID(id);
    }

    __declspec(dllexport) const char* CreateIdNew(int entityType) {
        static std::string id = EasyIDCreator::CreateId_New(entityType);
        return id.c_str();
    }

    __declspec(dllexport) long CreateId() {
        return EasyIDCreator::CreateId();
    }

}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

