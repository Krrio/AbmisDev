'use client';

import FullCalendar from '@fullcalendar/react';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';
import SidebarCalendar from '@/components/SidebarCalendar';


const Calendar = () => {
  return (
    <div className="grid grid-cols-1 md:grid-cols-5 h-screen">
      {/* Sidebar z listą wydarzeń */}
      <aside className="hidden md:block bg-gray-900 text-white md:col-span-1">
        <h2 className="text-xl font-bold mb-4">Events</h2>
        <SidebarCalendar />
      </aside>

      {/* Kalendarz główny */}
      <main className="bg-gray-100 p-4 md:col-span-4 grid-cols-4">
        <FullCalendar
          plugins={[dayGridPlugin, interactionPlugin]}
          initialView="dayGridWeek"
          headerToolbar={{
            left: 'prev,today,next',
            center: 'dayGridMonth,dayGridWeek',
            right: '', // Puste, jeśli chcesz dodać coś w przyszłości
          }}
          events={[
            { title: 'Spotkanie', start: '2025-01-22T10:00:00', color: '#4CAF50' },
            { title: 'Lunch', start: '2025-01-22T13:00:00', color: '#FFC107' },
          ]}
        />
      </main>
    </div>
  );
};

export default Calendar;
