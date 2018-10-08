package net.gogame.gowrap.integrations;

import java.util.Date;

public class PurchaseDetails {
    private String comment = null;
    private String currencyCode = null;
    private String orderId = null;
    private Double price = null;
    private String productId = null;
    private String purchaseData = null;
    private String referenceId = null;
    private boolean sandbox = false;
    private String signature = null;
    private Date timestamp = null;
    private VerificationStatus verificationStatus = null;

    public enum VerificationStatus {
        NOT_VERIFIED,
        VERIFICATION_SUCCEEDED,
        VERIFICATION_FAILED
    }

    public String getReferenceId() {
        return this.referenceId;
    }

    public void setReferenceId(String str) {
        this.referenceId = str;
    }

    public Date getTimestamp() {
        return this.timestamp;
    }

    public void setTimestamp(Date date) {
        this.timestamp = date;
    }

    public String getProductId() {
        return this.productId;
    }

    public void setProductId(String str) {
        this.productId = str;
    }

    public String getCurrencyCode() {
        return this.currencyCode;
    }

    public void setCurrencyCode(String str) {
        this.currencyCode = str;
    }

    public Double getPrice() {
        return this.price;
    }

    public void setPrice(Double d) {
        this.price = d;
    }

    public String getOrderId() {
        return this.orderId;
    }

    public void setOrderId(String str) {
        this.orderId = str;
    }

    public VerificationStatus getVerificationStatus() {
        return this.verificationStatus;
    }

    public void setVerificationStatus(VerificationStatus verificationStatus) {
        this.verificationStatus = verificationStatus;
    }

    public boolean isSandbox() {
        return this.sandbox;
    }

    public void setSandbox(boolean z) {
        this.sandbox = z;
    }

    public String getPurchaseData() {
        return this.purchaseData;
    }

    public void setPurchaseData(String str) {
        this.purchaseData = str;
    }

    public String getSignature() {
        return this.signature;
    }

    public void setSignature(String str) {
        this.signature = str;
    }

    public String getComment() {
        return this.comment;
    }

    public void setComment(String str) {
        this.comment = str;
    }
}
