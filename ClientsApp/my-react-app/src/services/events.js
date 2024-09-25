import axios from "axios";

export const fetchEvents = async (filter) => {
	try {
		var response = await axios.get("https://localhost:7033/controller", {
			params: {
				search: filter?.search,
				sortItem: filter?.sortItem,
				sortOrder: filter?.sortOrder,
			},
		});
		console.log(response.data)
		return response.data;
	} catch (e) {
		console.error(e);
	}
};

export const createEvents = async (event) => {
	try {
		var resposne = await axios.post("https://localhost:7033/controller", event);

		return resposne.status;
	} catch (e) {
		console.error(e);
	}
};
