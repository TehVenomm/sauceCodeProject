.class public interface abstract Lorg/onepf/oms/Appstore;
.super Ljava/lang/Object;
.source "Appstore.java"


# static fields
.field public static final PACKAGE_VERSION_UNDEFINED:I = -0x1


# virtual methods
.method public abstract areOutsideLinksAllowed()Z
.end method

.method public abstract getAppstoreName()Ljava/lang/String;
.end method

.method public abstract getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end method

.method public abstract getPackageVersion(Ljava/lang/String;)I
.end method

.method public abstract getProductPageIntent(Ljava/lang/String;)Landroid/content/Intent;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end method

.method public abstract getRateItPageIntent(Ljava/lang/String;)Landroid/content/Intent;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end method

.method public abstract getSameDeveloperPageIntent(Ljava/lang/String;)Landroid/content/Intent;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end method

.method public abstract isBillingAvailable(Ljava/lang/String;)Z
.end method

.method public abstract isPackageInstaller(Ljava/lang/String;)Z
.end method
