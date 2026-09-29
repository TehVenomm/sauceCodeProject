.class public Ljp/colopl/iab/IabBroadcastReceiver;
.super Landroid/content/BroadcastReceiver;
.source "IabBroadcastReceiver.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Ljp/colopl/iab/IabBroadcastReceiver$IabBroadcastListener;
    }
.end annotation


# instance fields
.field private final mListener:Ljp/colopl/iab/IabBroadcastReceiver$IabBroadcastListener;


# direct methods
.method public constructor <init>(Ljp/colopl/iab/IabBroadcastReceiver$IabBroadcastListener;)V
    .locals 0

    .line 12
    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    .line 13
    iput-object p1, p0, Ljp/colopl/iab/IabBroadcastReceiver;->mListener:Ljp/colopl/iab/IabBroadcastReceiver$IabBroadcastListener;

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 0

    .line 16
    iget-object p1, p0, Ljp/colopl/iab/IabBroadcastReceiver;->mListener:Ljp/colopl/iab/IabBroadcastReceiver$IabBroadcastListener;

    if-eqz p1, :cond_0

    .line 17
    iget-object p1, p0, Ljp/colopl/iab/IabBroadcastReceiver;->mListener:Ljp/colopl/iab/IabBroadcastReceiver$IabBroadcastListener;

    invoke-interface {p1}, Ljp/colopl/iab/IabBroadcastReceiver$IabBroadcastListener;->receivedBroadcast()V

    :cond_0
    return-void
.end method
