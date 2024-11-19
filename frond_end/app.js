document.getElementById('studentForm').addEventListener('submit', async (event) => {
    event.preventDefault();

    const studentId = document.getElementById('studentId').value;
    const classId = document.getElementById('classId').value;

    const url = `https://<your-function-url>/studentClass/${studentId}/${classId}`;

    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
        });

        if (response.ok) {
            document.getElementById('response').innerHTML = 'Thêm học sinh vào lớp thành công!';
        } else {
            document.getElementById('response').innerHTML = 'Có lỗi xảy ra. Vui lòng thử lại!';
        }
    } catch (error) {
        document.getElementById('response').innerHTML = 'Lỗi kết nối với máy chủ!';
        console.error(error);
    }
});
