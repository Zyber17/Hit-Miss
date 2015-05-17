#include <iostream>
#include <chrono>

#define len 10000000
#define iter 1000
int main()
{
    auto start = std::chrono::high_resolution_clock::now();
    std::cout << "Program Start" << std::endl;
    std::cout << "Creating data structures..." << std::endl;
    int* data = new int[len];
    int* pointerData = new int[len];
    int** dataP = new int*[len];
    int* missData = new int[len];
    int** dataM = new int*[len];
    std::cout << "Formatting data structures..." << std::endl;
    for (int i = 0; i < len; i++){
        data[i] = 1;
        pointerData[i] = 1;
        dataP[i] = &pointerData[i];
        missData[i] = 1;
        dataM[i] = &missData[((i % len)/2) + ((len/2) * (i%2))];
    }
    // test 0
    std::cout << "Test 0 - Packed Array: Initialized at t=0" << std::endl;
    auto init0 = std::chrono::high_resolution_clock::now();
    for (int a = 0; a < iter; a++){
    for (int i = 0; i < len; i++){
            data[i] += 3;
    }}
    auto elapsed0 = std::chrono::high_resolution_clock::now() - init0;
    long long microseconds0 = std::chrono::duration_cast<std::chrono::microseconds>(elapsed0).count();
    std::cout << "Test 0 - Packed Array: Terminated at t=" << microseconds0 << std::endl;
    //test 1
    std::cout << "Test 1 - Pointer Traversal: Initialized at t=0" << std::endl;
    auto init1 = std::chrono::high_resolution_clock::now();
    for (int a = 0; a < iter; a++){
    for (int i = 0; i < len; i++){
            *dataP[i] += 3;
    }}
    auto elapsed1 = std::chrono::high_resolution_clock::now() - init1;
    long long microseconds1 = std::chrono::duration_cast<std::chrono::microseconds>(elapsed1).count();
    std::cout << "Test 1 - Pointer Traversal: Terminated at t=" << microseconds1 << std::endl;
    //test 2
    std::cout << "Test 2 - Cache Miss: Initialized at t=0" << std::endl;
    auto init2 = std::chrono::high_resolution_clock::now();
    for (int a = 0; a < iter; a++){
    for (int i = 0; i < len; i++){
            *dataM[i] += 3;
    }}
    auto elapsed2 = std::chrono::high_resolution_clock::now() - init2;
    long long microseconds2 = std::chrono::duration_cast<std::chrono::microseconds>(elapsed2).count();
    std::cout << "Test 2 - Cache Miss: Terminated at t=" << microseconds2 << std::endl;
    return 0;
}
