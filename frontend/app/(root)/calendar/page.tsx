'use client';

import FullCalendar from '@fullcalendar/react';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';
import SidebarCalendar from '@/components/SidebarCalendar';

const Calendar = () => {
  return (
    <div className="flex justify-center items-center h-screen">
      {/* Kontener dla całej zawartości */}
      <div className="flex w-[90%] max-h-[80%] shadow-lg rounded-lg overflow-hidden">
        {/* Sidebar */}
        <aside className="w-1/4 bg-gray-900 text-white p-4 overflow-y-auto">
          <SidebarCalendar />
        </aside>

        {/* Kalendarz główny */}
        <main className="flex-1 bg-gray-100 p-4 overflow-y-auto">
          <FullCalendar
            plugins={[dayGridPlugin, timeGridPlugin, interactionPlugin]}
            initialView="timeGridWeek"
            slotDuration="01:00:00"
            headerToolbar={{
              left: 'prev,today,next',
              center: 'timeGridDay,timeGridWeek,dayGridMonth',
              right: '',
            }}
            allDaySlot={false}
            events={[
              { title: 'All-Hands Meeting', start: '2025-01-19T08:30:00', color: '#4CAF50' },
              { title: 'Monthly Catch-Up', start: '2025-01-19T09:30:00', color: '#FFC107' },
              { title: 'Quarterly Review', start: '2025-01-20T08:30:00', color: '#2196F3' },
              { title: 'Lunch with Client', start: '2025-01-22T13:00:00', color: '#2196F3' },
              { title: 'Design Review', start: '2025-01-22T16:00:00', color: '#FF5722' },
            ]}
            height="100%"
          />
        </main>
      </div>
    </div>
  );
};

export default Calendar;
