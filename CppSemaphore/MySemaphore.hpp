#pragma once

#ifndef MY_SEMAPHORE_HPP

#define MY_SEMAPHORE_HPP

#ifndef __cplusplus
#error The header <MySemaphore.hpp> is only for C++.
#endif

#include <cstdint>
#include <mutex>
#include <condition_variable>
#include <atomic>
#include <stdexcept>

class MySemaphore
{
public:
	MySemaphore(const MySemaphore&) = delete;
	MySemaphore& operator=(const MySemaphore&) = delete;
	~MySemaphore() = default;

	MySemaphore(::std::int32_t initCnt, ::std::int32_t maxCnt) : cnt(initCnt), max(maxCnt)
	{
		if (initCnt < 0 || maxCnt <= 0 || initCnt > maxCnt)
		{
			throw ::std::invalid_argument{"Invalid argument!"};
		}
	}

	void Down()
	{
		::std::unique_lock<::std::mutex> lg{ mtx };
		cv.wait(lg, [this] { return cnt != 0; });
		--cnt;
	}

	void Up()
	{
		::std::unique_lock<::std::mutex> lg{ mtx };
		if (cnt == max)
		{
			throw ::std::overflow_error{ "The semaphore is full!" };
		}
		++cnt;
		cv.notify_one();
	}

private:
	::std::atomic<::std::int32_t> cnt;
	::std::int32_t max;
	::std::condition_variable cv;
	mutable ::std::mutex mtx;
};

#endif // !MY_SEMAPHORE_HPP

