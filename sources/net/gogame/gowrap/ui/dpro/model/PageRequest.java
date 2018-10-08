package net.gogame.gowrap.ui.dpro.model;

public class PageRequest {
    private int pageNumber;
    private int pageSize;

    public PageRequest(int i, int i2) {
        this.pageNumber = i;
        this.pageSize = i2;
    }

    public int getPageNumber() {
        return this.pageNumber;
    }

    public void setPageNumber(int i) {
        this.pageNumber = i;
    }

    public int getPageSize() {
        return this.pageSize;
    }

    public void setPageSize(int i) {
        this.pageSize = i;
    }
}
