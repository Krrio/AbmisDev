import React from 'react';

const SidebarCalendar = () => {
  const events = [
    {
      date: 'Today',
      items: [
        { time: '8:30 - 9:00 AM', title: 'All-Hands Company Meeting', description: '' },
        { time: '9:30 - 10:00 AM', title: 'Monthly Catch-Up', description: '' },
      ],
    },
    {
      date: 'Tomorrow',
      items: [
        { time: '8:30 - 9:00 AM', title: 'Quarterly Review', description: 'Zoom link: https://zoom.us/1983475281' },
      ],
    },
  ];

  return (
    <div className="w-64 bg-gray-100 dark:bg-gray-900 p-4 rounded-lg shadow-md">
      <h2 className="text-lg font-bold mb-4 text-gray-800 dark:text-white">Events</h2>
      {events.map((event, index) => (
        <div key={index} className="mb-4">
          <h3 className="text-sm font-semibold text-gray-600 dark:text-gray-300">{event.date}</h3>
          {event.items.map((item, idx) => (
            <div key={idx} className="mt-2">
              <p className="text-sm font-medium text-gray-800 dark:text-white">{item.title}</p>
              <p className="text-xs text-gray-500 dark:text-gray-400">{item.time}</p>
              {item.description && (
                <p className="text-xs text-blue-500 dark:text-blue-300">{item.description}</p>
              )}
            </div>
          ))}
        </div>
      ))}
    </div>
  );
};

export default SidebarCalendar;
