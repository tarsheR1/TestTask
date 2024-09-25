import {
	Card,
	CardBody,
	CardFooter,
	CardHeader,
	Divider,
	Heading,
	Text,
} from "@chakra-ui/react";

export default function EventForm({ title, location, datetime }) {
	console.log(datetime);
	return (
		<Card variant={"filled"}>
			<CardHeader>
				<Heading size={"md"}>{title}</Heading>
			</CardHeader>
			<Divider borderColor={"gray"} />
			<CardBody>
				<Text>{location}</Text>
			</CardBody>
			<Divider borderColor={"gray"} />
			<CardFooter>{datetime}</CardFooter>
		</Card>
	);
}
