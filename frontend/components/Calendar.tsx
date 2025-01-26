import FullCalendar from '@fullcalendar/react';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';



const Calendar = () => {
  return (
    
    <FullCalendar
  plugins={[dayGridPlugin, interactionPlugin]}
  initialView="timeGridWeek"
  headerToolbar={{
    left: 'prev,today,next',
    center: 'title',
    right: 'dayGridMonth,timeGridWeek,timeGridDay',
  }}
  slotMinTime="00:00:00"
  slotMaxTime="24:00:00"
  allDaySlot={false} // Wyłączenie całodniowych wydarzeń, jeśli nie są używane
  height="auto" // Dynamiczna wysokość

  events={[
    { title: 'All-Hands Meeting', start: '2025-01-22T08:30:00', color: '#4CAF50' },
    { title: 'Monthly Catch-Up', start: '2025-01-22T09:30:00', color: '#FFC107' },
    { title: 'Lunch with Client', start: '2025-01-22T13:00:00', color: '#2196F3' },
    { title: 'Design Review', start: '2025-01-24T16:00:00', color: '#F44336' },
  ]}
/>

  );
};

export default Calendar;
