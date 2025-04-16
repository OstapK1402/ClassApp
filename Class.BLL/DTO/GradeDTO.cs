using System.ComponentModel.DataAnnotations;

namespace School.BLL.DTO
{
    public class GradeDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "������ �������� � ����'�������.")]
        public int HomeworkSubmissionId { get; set; }

        [Required(ErrorMessage = "������� � ����'�������.")]
        public int TeacherId { get; set; }

        [Range(1, 12, ErrorMessage = "������ ������� ���� �� 1 �� 12.")]
        public int GradeValue { get; set; }

        [StringLength(500, ErrorMessage = "�������� �� ������� ������������ 500 �������.")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "���� � ����'�������.")]
        public DateTime Date { get; set; }
    }
}