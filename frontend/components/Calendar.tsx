import FullCalendar from '@fullcalendar/react';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';
import input from './ui/input'; 


const Calendar = () => {
  return (
    
    <FullCalendar
      plugins={[dayGridPlugin, interactionPlugin]}
      initialView="dayGridWeek"
      headerToolbar={{
        left: 'prev,today,next',
        center: 'dayGridMonth,dayGridWeek',
        right: '', 
      }}
      events={[
        { title: 'Spotkanie', start: '2025-01-22T10:00:00', color: '#4CAF50' },
        { title: 'Lunch', start: '2025-01-22T13:00:00', color: '#FFC107' },
        // Dodaj więcej wydarzeń tutaj
      ]}
    />
  );
};

export default Calendar;
