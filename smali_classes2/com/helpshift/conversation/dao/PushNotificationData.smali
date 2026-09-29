.class public Lcom/helpshift/conversation/dao/PushNotificationData;
.super Ljava/lang/Object;
.source "PushNotificationData.java"


# instance fields
.field public final count:I

.field public final title:Ljava/lang/String;


# direct methods
.method public constructor <init>(ILjava/lang/String;)V
    .locals 0

    .line 7
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 8
    iput p1, p0, Lcom/helpshift/conversation/dao/PushNotificationData;->count:I

    .line 9
    iput-object p2, p0, Lcom/helpshift/conversation/dao/PushNotificationData;->title:Ljava/lang/String;

    return-void
.end method
