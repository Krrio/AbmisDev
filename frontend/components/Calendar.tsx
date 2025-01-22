'use client';

import React from 'react';
import FullCalendar from '@fullcalendar/react';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';

// Import niestandardowego stylu


const Calendar = () => {
  return (
    <div className="min-h-screen bg-slate-700 p-4">
      <h1 className="text-2xl font-bold mb-4 text-color-4">Planer</h1>
      <FullCalendar
        plugins={[dayGridPlugin, timeGridPlugin, interactionPlugin]}
        initialView="timeGridWeek"
        headerToolbar={{
          start: 'prev,next today',
          center: 'title',
          end: 'dayGridMonth,timeGridWeek,timeGridDay',
        }}
        events={[
          { title: 'Spotkanie', start: '2025-01-22T10:00:00', color: '#4CAF50' }, // Zielony
          { title: 'Lunch', start: '2025-01-22T13:00:00', color: '#FFC107' }, // Żółty
        ]}
        eventColor="#FF5722" // Domyślny kolor wydarzeń
        contentHeight="auto"
      />
    </div>
  );
};

export default Calendar;
