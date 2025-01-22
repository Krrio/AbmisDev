'use client';

import FullCalendar from '@fullcalendar/react'; // główny komponent
import dayGridPlugin from '@fullcalendar/daygrid'; // widok siatki
import '@fullcalendar/daygrid/main.css'; // styl dla dayGrid

const MiniCalendar = () => {
  return (
    <div className="bg-gray-800 p-4 rounded-md shadow">
      <FullCalendar
        plugins={[dayGridPlugin]}
        initialView="dayGridMonth"
        headerToolbar={false} // brak paska nawigacyjnego
        height="auto" // automatyczna wysokość
        dayMaxEventRows={true} // ograniczenie liczby wydarzeń na dzień
      />
    </div>
  );
};

export default MiniCalendar;
