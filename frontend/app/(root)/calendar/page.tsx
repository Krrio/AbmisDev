'use client';

import React from 'react';
import dynamic from 'next/dynamic';
import SidebarCalendar from '@/components/sidebarCalendar';

const Calendar = dynamic(() => import('@/components/Calendar'), { ssr: false });

const CalendarPage = () => {
  return (
    <div className="flex min-h-screen bg-gray-100 dark:bg-gray-900">
      {/* Sidebar */}
      <div className="w-1/4 p-4">
        <SidebarCalendar />
      </div>

      {/* Main Calendar */}
      <div className="w-3/4 p-4">
        <h1 className="text-2xl font-bold mb-4 text-gray-800 dark:text-white">Kalendarz</h1>
        <Calendar />
      </div>
    </div>
  );
};

export default CalendarPage;
