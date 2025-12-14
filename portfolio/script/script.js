// =============================
// Yardımcı: JSON'u index içinden oku
// =============================
function readJsonFromScriptTag(id) {
    const el = document.getElementById(id);
    if (!el) return null;
    try {
        return JSON.parse(el.textContent.trim());
    } catch (e) {
        console.warn(`${id} JSON parse edilemedi:`, e);
        return null;
    }
}

// =============================
// DİNAMİK YAZI (TYPING EFFECT)
// =============================
function getTypingPhrasesFromDOM() {
    const data = readJsonFromScriptTag("typing-phrases-data");
    return Array.isArray(data) && data.length ? data : [
        "Web geliştiricisiyim.",
        "Modern ve canlı arayüzler tasarlıyorum.",
        "Performans odaklı uygulamalar geliştiriyorum.",
        "Takım odaklı, temiz kod yazmayı seviyorum."
    ];
}

const typingPhrases = getTypingPhrasesFromDOM();
const typingElement = document.getElementById("typing-text");
let phraseIndex = 0;
let charIndex = 0;
let isDeleting = false;

function typeLoop() {
    const currentPhrase = typingPhrases[phraseIndex];

    if (!isDeleting) {
        typingElement.textContent = currentPhrase.slice(0, charIndex + 1);
        charIndex++;

        if (charIndex === currentPhrase.length) {
            isDeleting = true;
            setTimeout(typeLoop, 1400);
            return;
        }
    } else {
        typingElement.textContent = currentPhrase.slice(0, charIndex - 1);
        charIndex--;

        if (charIndex === 0) {
            isDeleting = false;
            phraseIndex = (phraseIndex + 1) % typingPhrases.length;
        }
    }

    const speed = isDeleting ? 50 : 90;
    setTimeout(typeLoop, speed);
}

// =============================
// PROFİL FOTOĞRAFI ÖNİZLEME
// =============================
function setupProfileUpload() {
    const fileInput = document.getElementById("profile-input");
    const img = document.getElementById("profile-photo");
    if (!fileInput || !img) return;

    fileInput.addEventListener("change", (e) => {
        const file = e.target.files?.[0];
        if (!file) return;

        const reader = new FileReader();
        reader.onload = (event) => {
            img.src = event.target.result;
        };
        reader.readAsDataURL(file);
    });
}

// =============================
// SKILL BARLARINI ANİMEE ET
// =============================
function animateSkills() {
    const skills = document.querySelectorAll(".skill");

    skills.forEach((skill) => {
        const percent = Number(skill.dataset.percent) || 0;
        const fill = skill.querySelector(".skill-fill");
        const label = skill.querySelector(".skill-percent");

        if (!fill || !label) return;

        requestAnimationFrame(() => {
            fill.style.width = percent + "%";
        });

        let current = 0;
        const duration = 900;
        const start = performance.now();

        function updateNumber(now) {
            const progress = Math.min((now - start) / duration, 1);
            current = Math.floor(progress * percent);
            label.textContent = current + "%";

            if (progress < 1) {
                requestAnimationFrame(updateNumber);
            } else {
                label.textContent = percent + "%";
            }
        }

        requestAnimationFrame(updateNumber);
    });
}

// =============================
// BECERİ KATEGORİ FİLTRELEME
// =============================
function setupSkillFilters() {
    const buttons = document.querySelectorAll(".filter-btn");
    const skills = document.querySelectorAll(".skill");

    buttons.forEach((btn) => {
        btn.addEventListener("click", () => {
            buttons.forEach((b) => b.classList.remove("active"));
            btn.classList.add("active");

            const filter = btn.dataset.filter;

            skills.forEach((skill) => {
                const category = skill.dataset.category;
                if (filter === "all" || category === filter) {
                    skill.style.display = "block";
                } else {
                    skill.style.display = "none";
                }
            });
        });
    });
}

// =============================
// PROJE TABLOSU - DİNAMİK VERİ
// =============================
function getProjectsDataFromDOM() {
    const data = readJsonFromScriptTag("projects-data");
    if (Array.isArray(data) && data.length) {
        return data;
    }
    return [
        {
            name: "Kişisel Portfolio Sitesi",
            category: "web",
            tech: "HTML, CSS, JavaScript",
            year: 2024
        }
    ];
}

let projectsData = getProjectsDataFromDOM();
let sortAscending = true;

function formatProjectCategory(category) {
    switch (category) {
        case "web":
            return "Web";
        case "mobile":
            return "Mobil";
        default:
            return "Diğer";
    }
}

function renderProjects(filter = "all") {
    const tbody = document.getElementById("projects-body");
    if (!tbody) return;

    tbody.innerHTML = "";

    const filtered = projectsData.filter((project) => {
        return filter === "all" || project.category === filter;
    });

    filtered.forEach((project) => {
        const tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${project.name}</td>
            <td>${formatProjectCategory(project.category)}</td>
            <td>${project.tech}</td>
            <td>${project.year}</td>
        `;
        tbody.appendChild(tr);
    });
}

function setupProjectFilters() {
    const buttons = document.querySelectorAll(".projects-filter-btn");

    buttons.forEach((btn) => {
        btn.addEventListener("click", () => {
            buttons.forEach((b) => b.classList.remove("active"));
            btn.classList.add("active");

            const filter = btn.dataset.projectFilter;
            renderProjects(filter);
        });
    });
}

function setupProjectSorting() {
    const sortBtn = document.getElementById("sort-year-btn");
    if (!sortBtn) return;

    sortBtn.addEventListener("click", () => {
        projectsData.sort((a, b) =>
            sortAscending ? a.year - b.year : b.year - a.year
        );
        sortAscending = !sortAscending;
        sortBtn.textContent = sortAscending
            ? "Yıla Göre Sırala"
            : "Yıla Göre Ters Sırala";

        const activeFilterBtn = document.querySelector(
            ".projects-filter-btn.active"
        );
        const currentFilter =
            activeFilterBtn?.dataset.projectFilter || "all";
        renderProjects(currentFilter);
    });
}

// =============================
// EĞİTİM LİSTESİ SCROLL ANİMASYONU
// =============================
function setupTimelineReveal() {
    const items = document.querySelectorAll(".timeline-item");
    if (!items.length) return;

    if ("IntersectionObserver" in window) {
        const observer = new IntersectionObserver(
            (entries) => {
                entries.forEach((entry) => {
                    if (entry.isIntersecting) {
                        entry.target.classList.add("is-visible");
                        observer.unobserve(entry.target);
                    }
                });
            },
            { threshold: 0.25 }
        );

        items.forEach((item) => observer.observe(item));
    } else {
        items.forEach((item) => item.classList.add("is-visible"));
    }
}

// =============================
// FOOTER YILI
// =============================
function setCurrentYear() {
    const yearSpan = document.getElementById("year");
    if (yearSpan) {
        yearSpan.textContent = new Date().getFullYear();
    }
}

// =============================
// SAYFA HAZIR OLDUĞUNDA
// =============================
document.addEventListener("DOMContentLoaded", () => {
    // Dinamik yazı
    typeLoop();

    // Profil fotoğrafı yükleme
    setupProfileUpload();

    // Beceriler
    setupSkillFilters();

    // Projeler
    setupProjectFilters();
    setupProjectSorting();
    renderProjects("all");

    // Eğitim timeline animasyonu
    setupTimelineReveal();

    // Footer yılı
    setCurrentYear();

    // Skill animasyonunu sadece bir kez tetikle
    const skillsSection = document.getElementById("skills");
    if ("IntersectionObserver" in window && skillsSection) {
        const observer = new IntersectionObserver(
            (entries) => {
                entries.forEach((entry) => {
                    if (entry.isIntersecting) {
                        animateSkills();
                        observer.disconnect();
                    }
                });
            },
            { threshold: 0.35 }
        );
        observer.observe(skillsSection);
    } else {
        animateSkills();
    }
});