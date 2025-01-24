import React from 'react';
import { AiOutlineClockCircle } from 'react-icons/ai'; // Ikona zegara

const SidebarCalendar = () => {
  const events = [
    {
      date: '2025-01-19',
      items: [
        { time: '8:30 - 9:00 AM', title: 'All-Hands Company Meeting', description: '' },
        { time: '9:30 - 10:00 AM', title: 'Monthly Catch-Up', description: '' },
      ],
    },
    {
      date: '2025-01-20',
      items: [
        { time: '8:30 - 9:00 AM', title: 'Quarterly Review', description: 'Zoom link: https://zoom.us/1983475281' },
      ],
    },
    {
      date: '2025-01-21',
      items: [
        { time: '10:00 - 11:00 AM', title: 'Team Stand-Up', description: '' },
        { time: '3:00 - 4:00 PM', title: 'Project Kick-Off', description: '' },
      ],
    },
    {
      date: '2025-01-22',
      items: [
        { time: '1:00 - 2:00 PM', title: 'Lunch with Client', description: '' },
        { time: '4:00 - 5:00 PM', title: 'Design Review', description: '' },
      ],
    },
    {
      date: '2025-01-23',
      items: [
        { time: '9:00 - 10:00 AM', title: 'Marketing Sync', description: '' },
        { time: '2:00 - 3:00 PM', title: 'Budget Planning', description: '' },
      ],
    },
    {
      date: '2025-01-24',
      items: [
        { time: '11:00 - 12:00 PM', title: 'One-on-One with Manager', description: '' },
        { time: '1:00 - 2:00 PM', title: 'Product Demo', description: '' },
      ],
    },
  ];

  return (
    <div className="w-64 bg-gray-900 p-4 rounded-lg shadow-md h-full overflow-y-auto">
      <h2 className="text-lg font-bold mb-4 text-white">Events</h2>
      {events.map((event, index) => (
        <div key={index} className="mb-4">
          <h3 className="text-sm font-semibold text-gray-400">{event.date}</h3>
          {event.items.map((item, idx) => (
            <div key={idx} className="mt-2 flex items-start gap-2">
              <AiOutlineClockCircle className="text-gray-500 mt-1" /> {/* Ikona zegara */}
              <div>
                <p className="text-sm font-medium text-white">{item.title}</p>
                <p className="text-xs text-gray-500">{item.time}</p>
                {item.description && (
                  <p className="text-xs text-blue-500">{item.description}</p>
                )}
              </div>
            </div>
          ))}
        </div>
      ))}
    </div>
  );
};

export default SidebarCalendar;
