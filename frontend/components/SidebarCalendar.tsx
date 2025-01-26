import React from 'react';
import { AiOutlineClockCircle } from 'react-icons/ai'; // Ikona zegara
import { FaChalkboardTeacher, FaUsers, FaLaptop, FaUtensils, FaChartLine } from 'react-icons/fa'; // Ikony dla wydarzeń
import { WiDaySunny, WiCloud, WiRain } from 'react-icons/wi'; // Ikony pogody

const SidebarCalendar = () => {
  const events = [
    {
      date: '2025-01-19',
      weather: { icon: <WiDaySunny className="text-yellow-400" />, temp: '15°C' },
      items: [
        { time: '8:30 - 9:00 AM', title: 'All-Hands Company Meeting', description: '', icon: <FaUsers className="text-green-500" /> },
        { time: '9:30 - 10:00 AM', title: 'Monthly Catch-Up', description: '', icon: <FaChartLine className="text-blue-500" /> },
      ],
    },
    {
      date: '2025-01-20',
      weather: { icon: <WiCloud className="text-gray-400" />, temp: '10°C' },
      items: [
        { time: '8:30 - 9:00 AM', title: 'Quarterly Review', description: 'Zoom link: https://zoom.us/1983475281', icon: <FaChalkboardTeacher className="text-purple-500" /> },
      ],
    },
    {
      date: '2025-01-21',
      weather: { icon: <WiRain className="text-blue-400" />, temp: '8°C' },
      items: [
        { time: '10:00 - 11:00 AM', title: 'Team Stand-Up', description: '', icon: <FaUsers className="text-yellow-500" /> },
        { time: '3:00 - 4:00 PM', title: 'Project Kick-Off', description: '', icon: <FaChartLine className="text-red-500" /> },
      ],
    },
    {
      date: '2025-01-22',
      weather: { icon: <WiDaySunny className="text-yellow-400" />, temp: '20°C' },
      items: [
        { time: '1:00 - 2:00 PM', title: 'Lunch with Client', description: '', icon: <FaUtensils className="text-orange-500" /> },
        { time: '4:00 - 5:00 PM', title: 'Design Review', description: '', icon: <FaLaptop className="text-indigo-500" /> },
      ],
    },
    {
      date: '2025-01-23',
      weather: { icon: <WiCloud className="text-gray-400" />, temp: '12°C' },
      items: [
        { time: '9:00 - 10:00 AM', title: 'Marketing Sync', description: '', icon: <FaChartLine className="text-teal-500" /> },
        { time: '2:00 - 3:00 PM', title: 'Budget Planning', description: '', icon: <FaChartLine className="text-purple-500" /> },
      ],
    },
    {
      date: '2025-01-24',
      weather: { icon: <WiRain className="text-blue-400" />, temp: '6°C' },
      items: [
        { time: '11:00 - 12:00 PM', title: 'One-on-One with Manager', description: '', icon: <FaUsers className="text-red-500" /> },
        { time: '1:00 - 2:00 PM', title: 'Product Demo', description: '', icon: <FaLaptop className="text-indigo-500" /> },
      ],
    },
    {
      date: '2025-02-01',
      weather: { icon: <WiDaySunny className="text-yellow-400" />, temp: '18°C' },
      items: [
        { time: '9:00 - 10:00 AM', title: 'Sprint Planning', description: '', icon: <FaUsers className="text-blue-500" /> },
        { time: '11:00 - 12:00 PM', title: 'Team Retrospective', description: '', icon: <FaChartLine className="text-green-500" /> },
      ],
    },
    {
      date: '2025-02-05',
      weather: { icon: <WiRain className="text-blue-400" />, temp: '4°C' },
      items: [
        { time: '2:00 - 3:00 PM', title: 'Client Presentation', description: '', icon: <FaChalkboardTeacher className="text-red-500" /> },
        { time: '4:00 - 5:00 PM', title: 'Budget Review', description: '', icon: <FaChartLine className="text-purple-500" /> },
      ],
    },
    {
      date: '2025-02-10',
      weather: { icon: <WiCloud className="text-gray-400" />, temp: '6°C' },
      items: [
        { time: '1:00 - 2:00 PM', title: 'Product Launch Prep', description: '', icon: <FaLaptop className="text-indigo-500" /> },
        { time: '3:30 - 4:30 PM', title: 'Marketing Meeting', description: '', icon: <FaChartLine className="text-teal-500" /> },
      ],
    },
    {
      date: '2025-02-15',
      weather: { icon: <WiDaySunny className="text-yellow-400" />, temp: '12°C' },
      items: [
        { time: '10:00 - 11:00 AM', title: 'Quarterly Townhall', description: '', icon: <FaUsers className="text-yellow-500" /> },
        { time: '1:00 - 2:00 PM', title: 'Stakeholder Update', description: '', icon: <FaChalkboardTeacher className="text-orange-500" /> },
      ],
    },
  ];

  return (
    <div className="w-64 bg-gray-900 p-4 rounded-lg shadow-md h-full overflow-y-auto hidden md:block">
      <h2 className="text-lg font-bold mb-4 text-white">Events</h2>
      {events.map((event, index) => (
        <div key={index} className="mb-6">
          <div className="flex items-center justify-between mb-2">
            <h3 className="text-sm font-semibold text-gray-400">{event.date}</h3>
            <div className="flex items-center gap-2">
              {event.weather.icon}
              <span className="text-xs text-gray-400">{event.weather.temp}</span>
            </div>
          </div>
          {event.items.map((item, idx) => (
            <div key={idx} className="mt-2 flex items-start gap-2">
              {item.icon} {/* Ikona wydarzenia */}
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
