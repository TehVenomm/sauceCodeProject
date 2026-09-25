.class public interface abstract Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;
.super Ljava/lang/Object;
.source "HSAndroidConnectivityManager.java"


# virtual methods
.method public abstract getConnectivityStatus()Lcom/helpshift/network/connectivity/HSConnectivityStatus;
    .annotation build Landroidx/annotation/NonNull;
    .end annotation
.end method

.method public abstract getConnectivityType()Lcom/helpshift/network/connectivity/HSConnectivityType;
    .annotation build Landroidx/annotation/NonNull;
    .end annotation
.end method

.method public abstract startListeningConnectivityChange(Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;)V
.end method

.method public abstract stopListeningConnectivityChange()V
.end method
