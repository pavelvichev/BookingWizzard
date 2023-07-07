document.addEventListener("click", function (e) {
	let addButton = document.getElementById('toggleButton');
	let deleteButton = document.getElementById('deleteButton');
	let content = document.getElementById('myContent');

	if (e.target === addButton) {
		content.style.display = content.style.display === 'none' ? 'block' : 'none';
	}
	else if (e.target === deleteButton) {
		let confirmation = confirm("Are you sure you want to delete the item?");
		if (!confirmation) {
			e.preventDefault();
		}
	}
});