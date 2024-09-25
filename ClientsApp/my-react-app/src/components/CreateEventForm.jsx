import { Button, Input, Textarea } from "@chakra-ui/react";
import { useState } from "react";

export default function CreateEventForm({ onCreate }) {
	const [event, setEvent] = useState();

	const onSubmit = (e) => {
		e.preventDefault();
		setEvent(null);
		onCreate(event);
	};

	return (
        <form onSubmit={onSubmit} className="w-full flex flex-col gap-3">
            <h3 className="font-bold text-xl">Создание мероприятия</h3>

            <Input
                placeholder="Название"
                value={event?.title ?? ""}
                onChange={(e) => setEvent({ ...event, title: e.target.value })}
            />

            <Textarea
                placeholder="Место проведения"
                value={event?.location ?? ""}
                onChange={(e) => setEvent({ ...event, location: e.target.value })}
            />

            <Input
                type="datetime-local"
                placeholder="Время проведения"
                value={event?.dateTime ?? ""}
                onChange={(e) => setEvent({ ...event, dateTime: e.target.value })}
            />

            <Button type="submit" colorScheme="teal">
                Создать
            </Button>
        </form>
	);
}
