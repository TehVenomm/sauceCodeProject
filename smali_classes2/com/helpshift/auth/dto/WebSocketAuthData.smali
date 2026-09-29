.class public Lcom/helpshift/auth/dto/WebSocketAuthData;
.super Ljava/lang/Object;
.source "WebSocketAuthData.java"

# interfaces
.implements Ljava/io/Serializable;


# instance fields
.field public final authToken:Ljava/lang/String;

.field public final webSocketRoute:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 10
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 11
    iput-object p1, p0, Lcom/helpshift/auth/dto/WebSocketAuthData;->authToken:Ljava/lang/String;

    .line 12
    iput-object p2, p0, Lcom/helpshift/auth/dto/WebSocketAuthData;->webSocketRoute:Ljava/lang/String;

    return-void
.end method
