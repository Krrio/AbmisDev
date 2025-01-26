'use client';

import FullCalendar from '@fullcalendar/react';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';
import SidebarCalendar from '@/components/SidebarCalendar';

const Calendar = () => {
  return (
    <div className="flex h-screen bg-gray-100">
      {/* Sidebar z listą wydarzeń */}
      <aside className="hidden md:block w-1/4 bg-gray-900 text-white overflow-y-auto">
        <SidebarCalendar />
      </aside>

      {/* Kalendarz główny */}
      <main className="w-full md:w-3/4 flex-grow bg-white p-4 overflow-hidden">
        <FullCalendar
          plugins={[dayGridPlugin, timeGridPlugin, interactionPlugin]}
          initialView="timeGridWeek"
          headerToolbar={{
            left: 'prev,today,next',
            center: 'dayGridMonth,timeGridWeek,timeGridDay',
            right: '',
          }}
          slotMinTime="00:00:00"
          slotMaxTime="24:00:00"
          allDaySlot={false}
          slotLabelFormat={{
            hour: 'numeric',
            minute: '2-digit',
            hour12: true, // Format 12-godzinny z odstępem
          }}
          height="100%"
          events={[
            { title: 'All-Hands Meeting', start: '2025-01-28T08:30:00', color: '#4CAF50' },
            { title: 'Monthly Catch-Up', start: '2025-01-29T09:30:00', color: '#FFC107' },
            { title: 'Lunch with Client', start: '2025-01-27T13:00:00', color: '#2196F3' },
            { title: 'Design Review', start: '2025-01-26T16:00:00', color: '#F44336' },
            { title: 'Project Kick-Off', start: '2025-02-01T10:00:00', color: '#673AB7' },
            { title: 'Team Sync', start: '2025-02-02T11:00:00', color: '#3F51B5' },
            { title: 'Marketing Review', start: '2025-02-03T15:00:00', color: '#009688' },
            { title: 'Budget Planning', start: '2025-02-04T14:00:00', color: '#FFC107' },
            { title: 'One-on-One with Manager', start: '2025-02-05T10:00:00', color: '#E91E63' },
            { title: 'Product Demo', start: '2025-02-06T13:00:00', color: '#795548' },
          ]}
        />
      </main>
    </div>
  );
};

export default Calendar;
