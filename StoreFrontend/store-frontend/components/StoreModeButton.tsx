import { FunctionComponent } from "react"
import Button from 'react-bootstrap/Button'

type StoreMode = "customer" | "admin"

const getMode: () => StoreMode = () => {
	if (typeof window !== "undefined" && typeof window.localStorage !== "undefined" && window.localStorage.storeMode === "admin") {
		return "admin"
	}

	return "customer"
}

const setMode: (mode: StoreMode) => StoreMode = (mode) => {
	if (typeof window === "undefined" || typeof window.localStorage === "undefined") {
		return "customer"
	}

	window.localStorage.storeMode = mode

	return window.localStorage.storeMode
}

const StoreModeButton: FunctionComponent = () => {
	const mode = getMode();
	const isCustomer = mode === "customer";

	return (
		<Button
			variant={isCustomer ? "info" : "warning"}
			onClick={() => setMode(isCustomer ? "admin" : "customer")}
		>
			{isCustomer ? "Клиент" : "Администратор"}
		</Button>
	)
}

export default StoreModeButton