import CreateEventForm from "./components/CreateEventForm";
import EventForm from "./components/EventForm";
import Filters from "./components/Filters";
import { createEvents, fetchEvents } from "./services/events.js";
import { useEffect, useState } from "react";

function App() {
    const [events, setEvents] = useState([]);
    const [filter, setFilter] = useState({
        search: "",
        sortItem: "date",
        sortOrder: "desc",
    });

    useEffect(() => {
        const fetchData = async () => {
            let events = await fetchEvents(filter);
            setEvents(events);
        };

        fetchData();
    }, [filter]);

     const onCreate = async (event) => {
       await createEvents(event);
       let events = await fetchEvents(filter);
       setEvents(events); 
     };

    return (
        <section className="p-8 flex flex-row justify-start items-start gap-12">
            <div className="flex flex-col w-1/3 gap-10">
                 <CreateEventForm onCreate={onCreate} /> 
                 <Filters filter={filter} setFilter={setFilter} /> 
            </div>

            <ul className="flex flex-col gap-5 w-1/2">
                {events.map((n) => (
                    <li key={n.id}>
                        <EventForm
                            title={n.title}
                            location={n.location}
                            datetime={n.eventDateTime}
                        />
                    </li>
                ))}
            </ul>
        </section>
    );
}

export default App;
