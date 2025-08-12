mergeInto(LibraryManager.library, {
    DownloadPdfFromSvg: function(svgStringPtr, fileNamePtr) {
        var svgString = UTF8ToString(svgStringPtr);
        var fileName = UTF8ToString(fileNamePtr);

        // Check if jsPDF is available.
        if (typeof window.jspdf === 'undefined' || typeof window.jspdf.jsPDF === 'undefined') {
            console.error("jsPDF is not loaded. Please ensure the library is included in the build.");
            return;
        }

        const { jsPDF } = window.jspdf;
        const doc = new jsPDF();

        // Check if the SVG plugin is available.
        if (typeof doc.svg !== 'function') {
            console.error("The jsPDF SVG plugin (svg2pdf.js) is not loaded. Please ensure it is included in the build.");
            return;
        }

        // The svg function requires the SVG as an element, not a string.
        const div = document.createElement('div');
        div.innerHTML = svgString;
        const svgElement = div.firstElementChild;

        if (!svgElement) {
            console.error("Could not parse SVG string.");
            return;
        }

        // Use the .svg() method to add the SVG to the PDF document.
        // It returns a promise.
        doc.svg(svgElement, {
            x: 15,
            y: 15,
            width: 180 // A4 is 210mm wide, leaving some margin.
        }).then(() => {
            // Save the PDF and trigger the download.
            doc.save(fileName);
        }).catch(e => {
            console.error("Error converting SVG to PDF:", e);
        });
    }
});
